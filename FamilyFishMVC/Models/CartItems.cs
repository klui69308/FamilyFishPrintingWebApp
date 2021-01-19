using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamilyFishMVC.Models
{
    public class CartItems
    {
        public int OrderID { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}