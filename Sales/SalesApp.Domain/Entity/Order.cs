using SalesApp.Domain.Enums;
using System;
using System.Collections.Generic;


namespace SalesApp.Domain.Entity
{
    public class Order
    {
        public Order()
        {
            OrderDate = DateTime.Now.Date;
            LineItems = new List<LineItem>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderSource OrderSource { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<LineItem> LineItems { get; set; }
    }
}
