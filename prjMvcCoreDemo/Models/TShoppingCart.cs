using System;
using System.Collections.Generic;

#nullable disable

namespace prjMvcCoreDemo.Models
{
    public partial class TShoppingCart
    {
        public int FId { get; set; }
        public string FDate { get; set; }
        public int? FCustomer { get; set; }
        public int? FProduct { get; set; }
        public int? FCount { get; set; }
        public decimal? FPrice { get; set; }
    }
}
