using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Infrastructure.Abstract;
using WebApplication5.Infrastructure.Entity;

namespace WebApplication5.Controllers
{
    public class PozitionController : Controller
    {
        private readonly IPozition pozition;

        public PozitionController(IPozition pozition)
        {
            this.pozition = pozition;
        }
        [HttpPost]
        public ActionResult LoadPozition(int? id)
        {
            IEnumerable<PozitionOrder> result = Enumerable.Empty<PozitionOrder>();
            if (id.HasValue)
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
            IEnumerable<FreePozitionOrder> result = pozition.GetFreePozition();
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

    }
}