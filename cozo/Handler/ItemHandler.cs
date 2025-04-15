using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cozo.Models;
using cozo.ViewModels;

namespace cozo.Handler
{
    public class ItemHandler
    {
        private StoreEntities1 db = new StoreEntities1();
        public List<ItemList> GetList(string SearchTerm = null, string SortOrder = null)
        {
            var Query = db.Items.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Query = Query.Where(i => i.Name.Contains(SearchTerm));
            }

            switch (SortOrder)
            {
                case "quantity":
                    Query = Query.OrderBy(i => i.Quuantity);
                    break;
                case "quantity_desc":
                    Query = Query.OrderByDescending(i => i.Quuantity);
                    break;
                case "price":
                    Query = Query.OrderBy(i => i.Price);
                    break;
                case "price_desc":
                    Query = Query.OrderByDescending(i => i.Price);
                    break;
                default:
                    Query = Query.OrderBy(i => i.Id);
                    break;
            }

            return Query
                .Select(Item => new ItemList
                {
                    Id = Item.Id,
                    Name = Item.Name,
                    Quantity = Item.Quuantity,
                    Price = Item.Price
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
                IsNew = true
            };
        }
        //public void Save(ItemPM model)
        //{
        //    if (model.IsNew)
        //    {
        //        int newId = db.Items.Any() ? db.Items.Max(i => i.Id) + 1 : 1;

        //        var newItem = new Item
        //        {
        //            Id = newId,
        //            Name = model.Name,
        //            Quuantity = model.Quantity,
        //            Price = (decimal)model.Price
        //        };

        //        db.Items.Add(newItem);
        //        model.Id = newId; // So controller can redirect
        //    }
        //    else
        //    {
        //        var item = db.Items.Find(model.Id);
        //        if (item != null)
        //        {
        //            item.Name = model.Name;
        //            item.Quuantity = model.Quantity;
        //            item.Price = (decimal)model.Price;
        //        }
        //    }

        //    db.SaveChanges();
        //}
        public void Save(ItemPM model)
        {
            var item = db.Items.Find(model.Id);

            if (item == null)
            {
                // Create new
                int newId = db.Items.Any() ? db.Items.Max(i => i.Id) + 1 : 1;

                var newItem = new Item
                {
                    Id = newId,
                    Name = model.Name,
                    Quuantity = model.Quantity,
                    Price = (decimal)model.Price
                };

                db.Items.Add(newItem);
                model.Id = newId; // So we can redirect to edit mode
            }
            else
            {
                // Update existing
                item.Name = model.Name;
                item.Quuantity = model.Quantity;
                item.Price = (decimal)model.Price;
            }

            db.SaveChanges();
        }


        public void Delete(int id)
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