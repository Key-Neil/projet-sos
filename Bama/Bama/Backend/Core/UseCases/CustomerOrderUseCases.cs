using System;
using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class CustomerOrderUseCases : ICustomerOrderUseCases
{
    private readonly ICustomerOrderGateway _customerOrderGateway;
    private readonly IBurgerGateway _burgerGateway;

    public CustomerOrderUseCases(ICustomerOrderGateway customerOrderGateway, IBurgerGateway burgerGateway)
    {
        _customerOrderGateway = customerOrderGateway ?? throw new ArgumentNullException(nameof(customerOrderGateway));
        _burgerGateway = burgerGateway ?? throw new ArgumentNullException(nameof(burgerGateway));
    }

    public void AddOrUpdateItemsToCustomerOrder(int customerOrderId, IEnumerable<OrderItem> items)
    {
        if (customerOrderId == 0)
        {
            throw new ArgumentException("Cart ID cannot be null.", nameof(customerOrderId));
        }

        if (items == null || !items.Any())
        {
            throw new ArgumentException("Items cannot be null or empty.", nameof(items));
        }

        foreach (var item in items)
        {
            if (item.Quantity <= 0)
            {
                throw new ArgumentException("Item quantity must be greater than zero.", nameof(item.Quantity));
            }

            var burger = _burgerGateway.GetBurgerById(item.Burger.BurgerId);

            if (burger == null)
            {
                throw new KeyNotFoundException($"Burger with ID {item.Burger.BurgerId} not found.");
            }

            if (item.Quantity > burger.Stock)
            {
                throw new ArgumentException($"Insufficient stock for burger {burger.Name}. Available: {burger.Stock}, Requested: {item.Quantity}.");
            }
        }

        _customerOrderGateway.AddOrUpdateItemsToCustomerOrder(customerOrderId, items);
    }

    public void ClearCustomerOrder(int customerId)
    {
        if (customerId == 0)
        {
            throw new ArgumentException("Customer ID cannot be null.", nameof(customerId));
        }

        var customerOrder = _customerOrderGateway.GetCustomerOrderByCustomerId(customerId);
        if (customerOrder == null)
        {
            throw new KeyNotFoundException("Customer Order not found.");
        }

        _customerOrderGateway.ClearCustomerOrder(customerOrder.CustomerOrderId);
    }

    public CustomerOrder GetCustomerOrderByCustomerId(int customerId)
    {
        if (customerId == 0)
        {
            throw new ArgumentException("Customer ID cannot be null.", nameof(customerId));
        }

        var customerOrder = _customerOrderGateway.GetCustomerOrderByCustomerId(customerId);

        if (customerOrder == null)
        {
            customerOrder = _customerOrderGateway.CreateCustomerOrder(customerId);
        }

        return customerOrder;
    }
   public void FinalizeOrder(int customerId)
    {
    // 1. On récupère la commande en cours
    var customerOrder = _customerOrderGateway.GetCustomerOrderByCustomerId(customerId);
    if (customerOrder == null || !customerOrder.OrderItems.Any())
    {
        throw new InvalidOperationException("Le panier est vide.");
    }

    // 2. On vérifie que le stock est suffisant pour chaque article (sécurité)
    foreach (var item in customerOrder.OrderItems)
    {
        var burger = _burgerGateway.GetBurgerById(item.Burger.BurgerId);
        if (burger == null || burger.Stock < item.Quantity)
        {
            throw new InvalidOperationException($"Stock insuffisant pour {item.Burger.Name}.");
        }
    }

    // 3. On met à jour le statut et le stock dans la base de données
    // (Note: dans une vraie application, on utiliserait une transaction ici)
    _customerOrderGateway.FinalizeOrder(customerOrder);

    // 4. On crée un nouveau panier vide pour le client
    _customerOrderGateway.CreateCustomerOrder(customerId);
    }
}
