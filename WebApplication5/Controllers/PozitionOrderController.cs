using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Infrastructure.Abstract;
using WebApplication5.Infrastructure.Entity;

namespace WebApplication5.Controllers
{
    public class PozitionOrderController : Controller
    {

        private readonly IPozitionOr pozition;

        public PozitionOrderController(IPozitionOr pozition)
        {
            this.pozition = pozition;
        }

        [HttpPost]
        public ActionResult LoadPozition(int? id)
        {
            var result = Enumerable.Empty<PozitionOrder>();
            if (id.HasValue)
            {
                result = pozition.GetPozition(id.Value);
            }
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void DeletePozition(int id)
        {
            pozition.DeletePozition(id);
        }

        [HttpPost]
        public ActionResult LoadAllPozition()
        {
            IEnumerable<PozitionFree> result = pozition.GetFreePozition();
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
        public void ChangePrice(int idPozition, double price, int idOrder)
        {
            pozition.ChangePrice(idPozition, price);
            pozition.CalculatePrice(idOrder);
            pozition.CalculateCostOrder(idOrder);
        }

        [HttpPost]
        public void ChangeNumberProduct(int idPozition, int numberProduct, int idOrder)
        {
            pozition.ChangeNumberProduct(idPozition, numberProduct, idOrder);
            pozition.CalculateCost(idPozition, idOrder);
        }

        //[HttpPost]
        //public void ChangeCost(int idPozition, double cost)
        //{
        //    pozition.ChangeCost(idPozition, cost);
        //}
    }
}