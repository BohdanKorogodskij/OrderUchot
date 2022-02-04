using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Infrastructure.Entity
{
    public class Pozition
    {
        public int ID { get; set; }
        public string NameProduct { get; set; }
        public decimal Price { get; set; }
        public int NumberProduct { get; set; }
        public decimal Cost { get; set; }
    }
}