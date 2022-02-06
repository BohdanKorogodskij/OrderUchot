using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication5.Infrastructure.Abstract;
using WebApplication5.Infrastructure.Entity;
using System.Data;

namespace WebApplication5.Infrastructure.Concrete
{
    public class OrderTable : IOrder
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

        public IEnumerable<Order> GetListTable()
        {
            var result = Enumerable.Empty<Order>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT
                                        list.ID
                                        ,list.DateOrder
                                        ,list.FIO
                                        ,ISNULL(SUM(list.CostOrder), 0) AS CostOrder
                                    FROM OrderList list
                                        GROUP BY list.ID, 
                                            list.DateOrder,
                                            list.FIO
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
                                var obj = new Order
                                { 
                                    ID = read.GetInt32(0),
                                    DateOrder = read.GetDateTime(1),
                                    FIO = read.GetString(2),
                                    CostOrder = (double)read.GetDecimal(3)
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

        public void Add(Order order)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"INSERT INTO [OrderList] (DateOrder, FIO, CostOrder) VALUES('{order.DateOrder.ToString("yyyy-MM-dd")}', '{order.FIO}', 0)";
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

        public void Delete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"DELETE FROM [OrderList] WHERE ID = {id}";
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

        public void Edit(Order order)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"UPDATE list SET DateOrder = '{order.DateOrder.ToString("yyyy-MM-dd")}', FIO = '{order.FIO}'
                                         FROM [OrderList] list 
                                            WHERE ID = {order.ID}";
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

        public Order GetOrder(int id)
        {
            IEnumerable<Order> result = Enumerable.Empty<Order>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"SELECT 
                                        list.[ID]
                                        ,list.[DateOrder]
                                        ,list.[FIO]
	                                    ,ISNULL(SUM(list.CostOrder), 0) as CostOrder
                                    FROM [OrderList] list
                                        WHERE list.ID = {id}
                                        GROUP BY list.ID, 
                                            list.DateOrder,
                                            list.FIO
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
                                var obj = new Order
                                {
                                    ID = read.GetInt32(0),
                                    DateOrder = read.GetDateTime(1),
                                    FIO = read.GetString(2),
                                    CostOrder = (double)read.GetDecimal(3)
                                };
                                result = result.Concat(new[] { obj });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result.First();
        }

        public IEnumerable<Order> GetPeriod(DateTime from, DateTime to)
        {
            IEnumerable<Order> result = Enumerable.Empty<Order>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"  SELECT 
                                            list.[ID]
                                            ,list.[DateOrder]
                                            ,list.[FIO]
	                                        ,ISNULL(SUM(list.CostOrder), 0) as CostOrder
                                        FROM [OrderList] list
                                            WHERE DateOrder BETWEEN '{from.ToString("yyyy-MM-dd")}' AND '{to.ToString("yyyy-MM-dd")}'
                                            GROUP BY list.ID, 
                                                list.DateOrder,
                                                list.FIO
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
                                var obj = new Order
                                {
                                    ID = read.GetInt32(0),
                                    DateOrder = read.GetDateTime(1),
                                    FIO = read.GetString(2),
                                    CostOrder = (double)read.GetDecimal(3)
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

        public void ChangeFIO(int idOrder, string fio)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"UPDATE orderList SET FIO = '{fio}'
                                        FROM [OrderList] orderList
                                            WHERE ID = {idOrder}";
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