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
        private readonly ITable table;
        private readonly IPozition pozition;

        public HomeController(ITable table, IPozition pozition)
        {
            this.table = table;
            this.pozition = pozition;
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
            //return View();
        }

        [HttpPost]
        public ActionResult LoadPozition(int? id)
        {
            IEnumerable<PozitionOrder> result = Enumerable.Empty<PozitionOrder>();
            if(id.HasValue)
            {
                result = pozition.GetPozition(id.Value);
            }
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void DeletePozition(int idOrder, int idPozition)
        {
            pozition.DeletePozition(idOrder, idPozition);
        }

        [HttpPost]
        public ActionResult LoadAllPozition()
        {
            IEnumerable<PozitionOrder> result = pozition.GetFreePozition();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddPozition(int idOrder, int idPozition)
        {
            pozition.AddPozition(idOrder, idPozition);
        }

        [HttpPost]
        public void ChangeNameProduct(int idPozition, string nameProduct)
        {
            pozition.ChangeNameProduct(idPozition, nameProduct);
        }

        [HttpPost]
        public void ChangePrice(int idPozition, double price)
        {
            pozition.ChangePrice(idPozition, price);
        }

        [HttpPost]
        public void ChangeNumberProduct(int idPozition, int numberProduct)
        {
            pozition.ChangeNumberProduct(idPozition, numberProduct);
        }

        [HttpPost]
        public void ChangeCost(int idPozition, double cost)
        {
            pozition.ChangeCost(idPozition, cost);
        }
        //+
        [HttpPost]
        public void ChangeFIO(int idOrder, string fio)
        {
            table.ChangeFIO(idOrder, fio);
        }
    }
}