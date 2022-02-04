using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Infrastructure.Entity
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime DateOrder { get; set; }
        public string FIO { get; set; }
        public double SumOrder { get; set; }
    }
}