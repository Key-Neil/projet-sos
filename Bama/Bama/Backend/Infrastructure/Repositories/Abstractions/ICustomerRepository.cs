using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions;

public interface ICustomerRepository
{
    Customer? GetCustomerByUsername(string username);
    void AddCustomer(Customer customer);
    IEnumerable<Customer> GetAllCustomers();
}
