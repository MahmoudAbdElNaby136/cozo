using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using cozo.Models;
using cozo.ViewModels;

namespace cozo.Handler
{
    public class ItemHandler
    {
        private  StoreEntities1 db = new StoreEntities1();
        public List<ItemDTO> GetList(ItemsList model)
        {
            int pagesize = 10;
            int pagenumber = model.Page;
            var query = db.Items.AsQueryable();
            if (!string.IsNullOrEmpty(model.SearchTerm))
            {
                query = query.Where(i => i.Name.Contains(model.SearchTerm));
            }
            // ✅ Calculate total pages before pagination
    int totalCount = query.Count();
            model.TotalPages = (int)Math.Ceiling((double)totalCount / pagesize);

            // Ensure valid page number
            if (pagenumber < 1) pagenumber = 1;
            if (pagenumber > model.TotalPages) pagenumber = model.TotalPages;
            switch (model.SortOrder)
            {
                case "quantity":
                    query = query.OrderBy(i => i.Quuantity);
                    break;
                case "quantity_desc":
                    query = query.OrderByDescending(i => i.Quuantity);
                    break;
                case "price":
                    query = query.OrderBy(i => i.Price);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(i => i.Price);
                    break;
                default:
                    query = query.OrderBy(i => i.Id);
                    break;
            }

            return query.Skip((pagenumber - 1) * pagesize)
                        .Take(pagesize)
                        .Select(item => new ItemDTO
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Quantity = item.Quuantity,
                            Price = item.Price
                        }).ToList();
        }

        public ItemPM GetSingle(int? id)
        {
            var item = db.Items.Find(id);
            if (item == null)
            {
                throw new Exception("Item not Found");
            }
            return new ItemPM
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quuantity,
                Price = item.Price,
                IsNew = false
            };
        }
        public void Save(ItemPM ItemPM)
        {
            if (ItemPM.IsNew)
            {
                int newId = db.Items.Any() ? db.Items.Max(i => i.Id) + 1 : 1;
                var NewItem = new Item
                {
                    Id = newId,
                    Name = ItemPM.Name,
                    Quuantity = ItemPM.Quantity,
                    Price = (decimal)ItemPM.Price
                };
                db.Items.Add(NewItem);
                ItemPM.Id = newId;
            }
            else
            {
                var item = db.Items.Find(ItemPM.Id);
                item.Name = ItemPM.Name;
                item.Quuantity = ItemPM.Quantity;
                item.Price = (decimal)ItemPM.Price;
            }
            db.SaveChanges();
        }

        public void Delete(int? id)
        {
            var item = db.Items.Find(id);
            if (item != null)
            {
                db.Items.Remove(item);
                db.SaveChanges();
            }
        }
    }
}