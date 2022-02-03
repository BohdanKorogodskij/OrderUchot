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
    public class Pozition : IPozition
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        public IEnumerable<PozitionOrder> GetPozition(int id)
        {
            IEnumerable<PozitionOrder> result = Enumerable.Empty<PozitionOrder>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"
                            SELECT 
                                pozition.ID, 
                                pozition.NameProduct, 
                                pozition.Price, 
                                pozition.NumberProduct, 
                                pozition.Cost 
                            FROM PozitionOrder pozition
                                JOIN Linker link ON pozition.ID = link.IDpozitionOrder
                                WHERE link.IDorderList = {id}
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

        public void DeletePozition(int idOrder, int idPozition)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"DELETE TOP (1) FROM [Linker] WHERE IDorderList = {idOrder} AND IDpozitionOrder = {idPozition}";
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

        public IEnumerable<PozitionOrder> GetFreePozition()
        {
            IEnumerable<PozitionOrder> result = Enumerable.Empty<PozitionOrder>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"
                            SELECT 
                                pozition.ID, 
                                pozition.NameProduct, 
                                pozition.Price, 
                                pozition.NumberProduct, 
                                pozition.Cost 
                            FROM PozitionOrder pozition
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