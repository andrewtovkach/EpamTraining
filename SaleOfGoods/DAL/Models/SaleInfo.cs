﻿using System;

namespace DAL.Models
{
    public class SaleInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Product Product { get; set; }
        public decimal Cost { get; set; }

        public SaleInfo(DateTime date, Client client, Product product, decimal cost, int id = 0)
        {
            Date = date;
            Client = client;
            Product = product;
            Cost = cost;
            Id = id;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} Client: {2} Product: {3} {4}", Id, Date.ToShortDateString(), Client, Product,  Cost);
        }
    }
}
