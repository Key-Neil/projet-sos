using Core.Models;

namespace Core.UseCases.Abstractions;

public interface ICustomerOrderUseCases
{
    CustomerOrder GetCustomerOrderByCustomerId(int customerId);
    void AddOrUpdateItemsToCustomerOrder(int customerOrderId, IEnumerable<OrderItem> items);
    void ClearCustomerOrder(int customerId);

    void FinalizeOrder(int customerId);
    
}
