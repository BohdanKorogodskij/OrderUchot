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
    public class Table : ITable
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

        public IEnumerable<OrderList> GetListTable()
        {
            IEnumerable<OrderList> result = Enumerable.Empty<OrderList>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT 
                                        list.[ID]
                                        ,list.[DateOrder]
                                        ,list.[FIO]
	                                    ,ISNULL(SUM(poz.Cost), 0) as SumOrder
                                    FROM [OrderList] list
                                        LEFT JOIN Linker link ON list.ID = link.IDorderList
                                        LEFT JOIN PozitionOrder poz ON link.IDpozitionOrder = poz.ID
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
                                var obj = new OrderList
                                { 
                                    ID = read.GetInt32(0),
                                    DateOrder = read.GetDateTime(1),
                                    FIO = read.GetString(2),
                                    SumOrder = (double)read.GetDecimal(3)
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

        public void Add(OrderList order)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"INSERT INTO [OrderList] (DateOrder, FIO) VALUES('{order.DateOrder.ToString("yyyy-MM-dd")}', '{order.FIO}')";
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

        public void Edit(OrderList order)
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

        public OrderList GetOrder(int id)
        {
            IEnumerable<OrderList> result = Enumerable.Empty<OrderList>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"SELECT 
                                        list.[ID]
                                        ,list.[DateOrder]
                                        ,list.[FIO]
	                                    ,ISNULL(SUM(poz.Cost), 0) as SumOrder
                                    FROM [OrderList] list
                                        LEFT JOIN Linker link ON list.ID = link.IDorderList
                                        LEFT JOIN PozitionOrder poz ON link.IDpozitionOrder = poz.ID
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
                                var obj = new OrderList
                                {
                                    ID = read.GetInt32(0),
                                    DateOrder = read.GetDateTime(1),
                                    FIO = read.GetString(2),
                                    SumOrder = (double)read.GetDecimal(3)
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

        public IEnumerable<OrderList> GetPeriod(DateTime from, DateTime to)
        {
            IEnumerable<OrderList> result = Enumerable.Empty<OrderList>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $@"SELECT 
                                        list.[ID]
                                        ,list.[DateOrder]
                                        ,list.[FIO]
	                                    ,ISNULL(SUM(poz.Cost), 0) as SumOrder
                                    FROM [OrderList] list
                                        LEFT JOIN Linker link ON list.ID = link.IDorderList
                                        LEFT JOIN PozitionOrder poz ON link.IDpozitionOrder = poz.ID
                                    WHERE DateOrder BETWEEN '{from.ToString("yyyy-MM-dd")}' AND '{to.ToString("yyyy-MM-dd")}'
                                    GROUP BY list.ID, 
                                        list.DateOrder,
                                        list.FIO";
                    using (var command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;
                        command.CommandTimeout = int.MaxValue;
                        using (var read = command.ExecuteReader())
                        {
                            while(read.Read())
                            {
                                var obj = new OrderList
                                {
                                    ID = read.GetInt32(0),
                                    DateOrder = read.GetDateTime(1),
                                    FIO = read.GetString(2),
                                    SumOrder = (double)read.GetDecimal(3)
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