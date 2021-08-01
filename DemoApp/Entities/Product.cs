using System;
using System.Collections.Generic;

#nullable disable

namespace DemoApp.Entities
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public int CategoryId { get; set; }
        public sbyte Active { get; set; }

        public virtual Category Category { get; set; }
    }
}
