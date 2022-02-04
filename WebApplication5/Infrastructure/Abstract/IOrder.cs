using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication5.Infrastructure.Entity;

namespace WebApplication5.Infrastructure.Abstract
{
    public interface IOrder
    {
        IEnumerable<Order> GetListTable();
        void Add(Order order);
        void Delete(int id);
        void Edit(Order order);
        Order GetOrder(int id);
        IEnumerable<Order> GetPeriod(DateTime from, DateTime to);
        void ChangeFIO(int idOrder, string fio);
    }
}
