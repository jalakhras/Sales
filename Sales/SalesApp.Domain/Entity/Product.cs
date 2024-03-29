﻿using System;
using System.Collections.Generic;

namespace SalesApp.Domain.Entity
{
    public class Product
    {
        public Product()
        {
            IsAvailable = true;
            LineItems = new HashSet<LineItem>();
            Categories = new HashSet<Category>();
        }

        public int ProductId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public DateTime ProductionStart { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<LineItem> LineItems { get; set; }
        public ICollection<Category> Categories { get; set; }
        public void RemoveFromProduction()
        {
            IsAvailable = false;
        }
    }
}
