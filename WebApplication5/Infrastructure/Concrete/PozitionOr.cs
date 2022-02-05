using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication5.Infrastructure.Abstract;
using WebApplication5.Infrastructure.Entity;

namespace WebApplication5.Infrastructure.Concrete
{
    public class PozitionOr : IPozitionOr
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        public IEnumerable<PozitionOrder> GetPozition(int id)
        {
            var result = Enumerable.Empty<PozitionOrder>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"
                                    select 
                                        pozition.ID,
                                        poz.NameProduct,
                                        poz.Price,
                                        pozition.NumberProduct,
                                        pozition.Cost
                                    from PozitionOrder pozition
                                        left join Pozition poz ON pozition.IDpozition = poz.ID
                                WHERE pozition.IDorder = {id}
                        ";
                    using (var command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;
                        command.CommandTimeout = int.MaxValue;
                        using (var read = command.ExecuteReader())
                        {
                            while(read.Read())
                            {
                                var obj = new PozitionOrder
                                {
                                    ID = read.GetInt32(0),
                                    NameProduct = read.GetString(1),
                                    Price = read.GetDecimal(2),
                                    NumberProduct = read.GetInt32(3),
                                    Cost = read.GetDecimal(4)
                                };
                                result = result.Concat(new[] { obj });
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {

            }
            return result;
        }

        public void DeletePozition(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"DELETE FROM [PozitionOrder] WHERE ID = {id}";
                    using (var command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;
                        command.CommandTimeout = int.MaxValue;
                        var result = command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                
            }
        }

        public IEnumerable<PozitionFree> GetFreePozition()
        {
            var result = Enumerable.Empty<PozitionFree>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"
                            SELECT 
                                pozition.ID, 
                                pozition.NameProduct, 
                                pozition.Price
                            FROM Pozition pozition
                        ";
                    using (var command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;
                        command.CommandTimeout = int.MaxValue;
                        using (var read = command.ExecuteReader())
                        {
                            while (read.Read())
                            {
                                var obj = new PozitionFree
                                {
                                    ID = read.GetInt32(0),
                                    NameProduct = read.GetString(1),
                                    Price = read.GetDecimal(2)
                                };
                                result = result.Concat(new[] { obj });
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return result;
        }

        public void AddPozition(int idOrder, int idPozition)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"INSERT INTO [Linker] (IDorderList, IDpozitionOrder) VALUES({idOrder}, {idPozition})";
                    using (var command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;
                        command.CommandTimeout = int.MaxValue;
                        var result = command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void ChangeNameProduct(int idPozition, string nameProduct)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"UPDATE pozition SET NameProduct = '{nameProduct}'
                                        FROM [PozitionOrder] pozition
                                            WHERE ID = {idPozition}";
                    using (var command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;
                        command.CommandTimeout = int.MaxValue;
                        var result = command.ExecuteNonQuery();
                    }
                }
            }catch(Exception ex)
            {

            }
        }

        public void ChangePrice(int idPozition, double price)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"UPDATE pozition SET Price = {price}
                                        FROM [PozitionOrder] pozition
                                            WHERE ID = {idPozition}";
                    using (var command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;
                        command.CommandTimeout = int.MaxValue;
                        var result = command.ExecuteNonQuery();
                    }
                }
            }catch(Exception ex)
            {

            }
        }

        public void ChangeNumberProduct(int idPozition, int numberProduct)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"UPDATE pozition SET NumberProduct = {numberProduct}
                                         FROM [PozitionOrder] pozition
                                            WHERE ID = {idPozition}";
                    using (var command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;
                        command.CommandTimeout = int.MaxValue;
                        var result = command.ExecuteNonQuery();
                    }
                }
            }catch(Exception ex)
            {

            }
        }

        public void ChangeCost(int idPozition, double cost)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"UPDATE pozition SET Cost = {cost}
                                        FROM [PozitionOrder] pozition
                                            WHERE ID = {idPozition}";
                    using (var command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;
                        command.CommandTimeout = int.MaxValue;
                        var result = command.ExecuteNonQuery();
                    }
                }
            }catch(Exception ex)
            {

            }
        }
    }
}