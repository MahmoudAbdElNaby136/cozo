﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cozo.ViewModels
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}