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

    /// <summary>
    /// Ajoute un nouveau client dans la base de données avec mot de passe hashé
    /// </summary>
    public void AddCustomer(string username, string passwordHash)
    {
        var customer = new Customer
        {
            Username = username,
            PasswordHash = passwordHash
        };

        _customerRepository.AddCustomer(customer);
    }

    /// <summary>
    /// Récupère tous les clients (version "métier")
    /// </summary>
    public IEnumerable<Core.Models.Customer> GetAllCustomers()
    {
        var customers = _customerRepository.GetAllCustomers();
        return customers.Select(customer => new Core.Models.Customer
        {
            CustomerId = customer.CustomerId,
            Username = customer.Username,
            PasswordHash = customer.PasswordHash,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber
        });
    }

    /// <summary>
    /// Récupère uniquement le hash du mot de passe d'un utilisateur (authentification)
    /// </summary>
    public string? GetCustomerPasswordHash(string username)
    {
        var customer = _customerRepository.GetCustomerByUsername(username);
        return customer?.PasswordHash;
    }

    /// <summary>
    /// Récupère un utilisateur complet à partir de son nom d’utilisateur
    /// </summary>
    public Core.Models.Customer? GetCustomerByUsername(string username)
    {
        var infraCustomer = _customerRepository.GetCustomerByUsername(username);
        if (infraCustomer == null) return null;

        return new Core.Models.Customer
        {
            CustomerId = infraCustomer.CustomerId,
            Username = infraCustomer.Username,
            PasswordHash = infraCustomer.PasswordHash,
            FirstName = infraCustomer.FirstName,
            LastName = infraCustomer.LastName,
            Email = infraCustomer.Email,
            PhoneNumber = infraCustomer.PhoneNumber
        };
    }

    /// <summary>
    /// Met à jour les informations personnelles d’un client (sauf le mot de passe ici)
    /// </summary>
    public void UpdateCustomer(Core.Models.Customer customer)
    {
        var infraCustomer = new Infrastructure.Models.Customer
        {
            CustomerId = customer.CustomerId,
            Username = customer.Username,
            // ⚠️ On ignore le mot de passe ici volontairement pour ne pas l’écraser
            PasswordHash = "", 
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber
        };

        _customerRepository.UpdateCustomer(infraCustomer);
    }
}
