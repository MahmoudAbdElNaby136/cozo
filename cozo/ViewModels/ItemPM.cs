using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cozo.ViewModels
{
    public class ItemPM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage = "Quantity must be greater than 0")]

        public decimal? Price { get; set; }

        public bool IsNew { get; set; }
        public static ItemPM Create()
        {
            return new ItemPM() { IsNew = true };
        }
    }
}