using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private readonly string _connectionString;

        public CustomerOrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "Database connection string 'DefaultConnection' not found.");
        }

        private MySqlConnection GetConnection() => new MySqlConnection(_connectionString);

        public CustomerOrder? GetCustomerOrderByCustomerId(int customerId)
        {
            using var connection = GetConnection();
            var sql = "SELECT * FROM CustomerOrder WHERE CustomerId = @CustomerId";
            var customerOrder = connection.QuerySingleOrDefault<CustomerOrder>(sql, new { CustomerId = customerId });

            if (customerOrder != null)
            {
                var orderItemSql = "SELECT * FROM OrderItem WHERE CustomerOrderId = @CustomerOrderId";
                var orderItems = connection.Query<OrderItem>(orderItemSql, new { CustomerOrderId = customerOrder.CustomerOrderId });
                customerOrder.OrderItems = [.. orderItems];
            }
            return customerOrder;
        }

        public CustomerOrder CreateCustomerOrder(int customerId)
        {
            using var connection = GetConnection();

            var sql = "INSERT INTO CustomerOrder (CustomerId) VALUES (@CustomerId);";

            connection.Execute(sql, new { CustomerId = customerId });

            return GetCustomerOrderByCustomerId(customerId) ?? throw new InvalidOperationException("Failed to create customer order.");
        }

        public void AddItemsToCustomerOrder(int customerOrderId, IEnumerable<OrderItem> items)
        {
            using var connection = GetConnection();

            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                foreach (var item in items)
                {
                    var orderItem = new OrderItem
                    {
                        CustomerOrderId = customerOrderId,
                        BurgerId = item.BurgerId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    };
                    var sql = "INSERT INTO OrderItem (CustomerOrderId, BurgerId, Quantity, Price) VALUES (@CustomerOrderId, @BurgerId, @Quantity, @Price)";
                    connection.Execute(sql, orderItem, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void ClearCustomerOrder(int customerOrderId)
        {
            using var connection = GetConnection();
            var sql = "DELETE FROM OrderItem WHERE CustomerOrderId = @CustomerOrderId";
            connection.Execute(sql, new { CustomerOrderId = customerOrderId });
        }

        public void UpdateItemsInCustomerOrder(int customerOrderId, IEnumerable<OrderItem> items)
        {
            using var connection = GetConnection();

            connection.Open();

            var transaction = connection.BeginTransaction();

            try
            {
                foreach (var item in items)
                {
                    var orderItem = new OrderItem
                    {
                        OrderItemId = customerOrderId,
                        BurgerId = item.BurgerId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    };
                    
                    var sql = "UPDATE OrderItem SET Quantity = @Quantity, Price = @Price WHERE CustomerOrderId = @CustomerOrderId AND BurgerId = @BurgerId";
                    connection.Execute(sql, orderItem, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public OrderItem? GetOrderItemById(int customerOrderId, int burgerId)
        {
            using var connection = GetConnection();
            var sql = "SELECT * FROM OrderItem WHERE CustomerOrderId = @CustomerOrderId AND BurgerId = @BurgerId";
            return connection.QuerySingleOrDefault<OrderItem>(sql, new { CustomerOrderId = customerOrderId, BurgerId = burgerId });
        }
    }
}
