using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication5.Infrastructure.Entity;

namespace WebApplication5.Infrastructure.Abstract
{
    public interface ITable
    {
        IEnumerable<OrderList> GetListTable();
        void Add(OrderList order);
        void Delete(int id);
        void Edit(OrderList order);
        OrderList GetOrder(int id);
        IEnumerable<OrderList> GetPeriod(DateTime from, DateTime to);
        void ChangeFIO(int idOrder, string fio);
    }
}
