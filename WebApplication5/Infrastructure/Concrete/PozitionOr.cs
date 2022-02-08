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
                                    SELECT 
                                        pozition.ID,
                                        poz.NameProduct,
                                        poz.Price,
                                        pozition.NumberProduct,
                                        pozition.Cost
                                    FROM PozitionOrder pozition
                                        LEFT JOIN Pozition poz ON pozition.IDpozition = poz.ID
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
                                    ID = GetInt(read["ID"]),
                                    NameProduct = GetString(read["NameProduct"]),
                                    Price = GetDecimal(read["Price"]),
                                    NumberProduct = GetInt(read["NumberProduct"]),
                                    Cost = GetDecimal(read["Cost"])
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
                                    ID = GetInt(read["ID"]),
                                    NameProduct = GetString(read["NameProduct"]),
                                    Price = GetDecimal(read["Price"])
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
                    string query = $"INSERT INTO [PozitionOrder] (IDorder, IDpozition, NumberProduct, Cost) VALUES({idOrder}, {idPozition}, 0, 0)";
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
                                        FROM [Pozition] pozition
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
        public void CalculatePrice(int idOrder)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"UPDATE pozOrder SET Cost = ISNULL(pozOrder.NumberProduct * poz.Price, 0)
                                       FROM PozitionOrder pozOrder
                                       LEFT JOIN Pozition poz ON pozOrder.IDpozition = poz.ID
                                       WHERE pozOrder.IDorder = {idOrder}
                                    ";
                    using (var command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;
                        command.CommandTimeout = int.MaxValue;
                        var result = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void CalculateCostOrder(int idOrder)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"
                                    DECLARE @CostOrder MONEY
	                                    SELECT
	                                        @CostOrder = ISNULL(SUM(pozOrder.Cost), 0)
	                                    FROM OrderList list
	                                    LEFT JOIN PozitionOrder pozOrder ON list.ID = pozOrder.IDorder
	                                        WHERE list.ID = {idOrder}

                                    UPDATE list SET CostOrder = @CostOrder
                                       FROM OrderList list
                                            WHERE list.ID = {idOrder}
                                    ";
                    using (var command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;
                        command.CommandTimeout = int.MaxValue;
                        var result = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
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
                                        FROM [Pozition] pozition
                                            WHERE ID = {idPozition}
                                    ";
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
        public void CalculateCost(int idPozition, int idOrder)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"
                                    DECLARE @Cost MONEY,
		                                    @CostOrder MONEY

		                                    SELECT 
			                                    @Cost = poz.Price * pozOrder.NumberProduct
			                                FROM PozitionOrder pozOrder
			                                LEFT JOIN Pozition poz ON pozOrder.IDpozition = poz.ID
			                                    WHERE pozOrder.ID = {idPozition}

                                    UPDATE poz SET Cost = @Cost FROM PozitionOrder poz WHERE ID = {idPozition}

                                            SELECT 
			                                    @CostOrder = ISNULL(SUM(pozOrder.Cost), 0)
			                                FROM PozitionOrder pozOrder
			                                LEFT JOIN OrderList list ON pozOrder.IDorder = list.ID
			                                    WHERE list.ID = {idOrder}

                                    UPDATE pozOrder SET CostOrder = @CostOrder FROM OrderList pozOrder WHERE ID = {idOrder}
                                    ";
                    using (var command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;
                        command.CommandTimeout = int.MaxValue;
                        var result = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        
        public void ChangeNumberProduct(int idPozition, int numberProduct, int idOrder)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"UPDATE pozition SET NumberProduct = {numberProduct}
                                         FROM [PozitionOrder] pozition
                                            WHERE ID = {idPozition}
                                    ";
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


        private int GetInt(object obj)
        {
            var result = 0;
            if (int.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            return result;
        }

        private decimal GetDecimal(object obj)
        {
            var result = 0M;
            if(decimal.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            return result;
        }

        private string GetString(object obj)
        {
            if(obj == null)
            {
                return string.Empty;
            } else
            {
                return obj.ToString();
            }
        }
    }
}