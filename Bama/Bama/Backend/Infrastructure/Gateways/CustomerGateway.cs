using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class CustomerGateway : ICustomerGateway
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerGateway(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public void AddCustomer(string username, string passwordHash)
    {
        var customer = new Customer
        {
            Username = username,
            PasswordHash = passwordHash
        };

        _customerRepository.AddCustomer(customer);
    }

    public IEnumerable<Core.Models.Customer> GetAllCustomers()
    {
        var customers = _customerRepository.GetAllCustomers();
        return customers.Select(customer => new Core.Models.Customer
        {
            CustomerId = customer.CustomerId,
            Username = customer.Username
        });
    }

    public string? GetCustomerPasswordHash(string username) 
    {
        var customer = _customerRepository.GetCustomerByUsername(username);
        return customer?.PasswordHash;
    }

    public Core.Models.Customer? GetCustomerByUsername(string username)
    {
        var infraCustomer = _customerRepository.GetCustomerByUsername(username);
        if (infraCustomer == null) return null;
        return new Core.Models.Customer
        {
            CustomerId = infraCustomer.CustomerId,
            Username = infraCustomer.Username
        };
    }
}
