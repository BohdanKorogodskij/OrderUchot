using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication5.Infrastructure.Entity;

namespace WebApplication5.Infrastructure.Abstract
{
    public interface IPozitionOr
    {
        IEnumerable<PozitionOrder> GetPozition(int id);
        void DeletePozition(int id);
        IEnumerable<PozitionFree> GetFreePozition();
        void AddPozition(int idOrder, int idPozition);
        void ChangeNameProduct(int idPozition, string nameProduct);
        void ChangePrice(int idPozition, double price);
        void ChangeNumberProduct(int idPozition, int numberProduct, int idOrder);
        void ChangeCost(int idPozition, double cost);
    }
}
