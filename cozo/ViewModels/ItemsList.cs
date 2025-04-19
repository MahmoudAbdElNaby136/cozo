using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using cozo.Models;

namespace cozo.ViewModels
{
    public class ItemsList
    {
        StoreEntities1 db = new StoreEntities1();
        public string SearchTerm { get; set; }
        public string SortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int TotalPages { get; set; } 
        public List<ItemDTO> Items { get; set; }
    }
}