﻿namespace SalesApp.Domain.Entity
{
    public class ContactDetail
    {
        public int Id { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string OfficePhone { get; set; }
        public string TwitterAlias { get; set; }
        public string Facebook { get; set; }
        public string LinkedIn { get; set; }
        public string Skype { get; set; }
        public string Messenger { get; set; }
        public int CustomerId { get; set; }
    }
}
