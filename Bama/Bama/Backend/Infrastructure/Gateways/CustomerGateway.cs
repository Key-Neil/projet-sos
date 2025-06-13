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
            Username = infraCustomer.Username,
            FirstName = infraCustomer.FirstName,
            LastName = infraCustomer.LastName,
            Email = infraCustomer.Email,
            PhoneNumber = infraCustomer.PhoneNumber
        };
    }
    
    public void UpdateCustomer(Core.Models.Customer customer)
{
    var infraCustomer = new Infrastructure.Models.Customer
    {
        CustomerId = customer.CustomerId,
        Username = customer.Username,
        PasswordHash = "", // Tu peux l'ignorer ici si tu ne mets pas à jour le mot de passe
        FirstName = customer.FirstName,
        LastName = customer.LastName,
        Email = customer.Email,
        PhoneNumber = customer.PhoneNumber
    };

    _customerRepository.UpdateCustomer(infraCustomer);
}

}
