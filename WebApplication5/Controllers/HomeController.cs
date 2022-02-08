using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Infrastructure.Abstract;
using WebApplication5.Infrastructure.Entity;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrder table;

        public HomeController(IOrder table)
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
            IEnumerable<Order> result = Enumerable.Empty<Order>();
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
            Order order = table.GetOrder(id);
            return View(order);
        }

        

        [HttpPost]
        public void DeleteTable(int id)
        {
            table.Delete(id);
        }

        [HttpPost]
        public void SaveEdit(Order order)
        {
            table.Edit(order);
        }

        [HttpGet]
        public ActionResult Add(Order order)
        {
            var id = table.Add(order);
            order.ID = id;
            return View(order);
        }
        
        [HttpPost]
        public void ChangeFIO(int idOrder, string fio)
        {
            table.ChangeFIO(idOrder, fio);
        }
    }
}