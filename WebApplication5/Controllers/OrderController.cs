using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Infrastructure.Abstract;
using WebApplication5.Infrastructure.Entity;

namespace WebApplication5.Controllers
{
    public class OrderController : Controller
    {
        private readonly ITable table;
        

        public OrderController(ITable table)
        {
            this.table = table;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult LoadTable(DateTime? from, DateTime? to)
        {
            IEnumerable<OrderList> result = Enumerable.Empty<OrderList>();
            if(from.HasValue && to.HasValue)
            {
                result = table.GetPeriod(from.Value, to.Value);
            }
            else
            {
                result = table.GetListTable();
            }
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditTable(int id)
        {
            OrderList order = table.GetOrder(id);
            return View(order);
        }

        [HttpPost]
        public void DeleteTable(int id)
        {
            table.Delete(id);
        }

        [HttpPost]
        public void SaveEdit(OrderList order)
        {
            table.Edit(order);
        }

        [HttpPost]
        public void Add(OrderList order)
        {
            table.Add(order);
        }
        
        [HttpPost]
        public void ChangeFIO(int idOrder, string fio)
        {
            table.ChangeFIO(idOrder, fio);
        }
    }
}