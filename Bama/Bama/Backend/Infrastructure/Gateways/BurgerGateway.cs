using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Gateways;

public class BurgerGateway : IBurgerGateway
{
    private readonly IBurgerRepository _burgerRepository;

    public BurgerGateway(IBurgerRepository burgerRepository)
    {
        _burgerRepository = burgerRepository;
    }

    public IEnumerable<Core.Models.Burger> GetAllBurgers()
    {
        var infraBurgers = _burgerRepository.GetAllBurgers();
        var coreBurgers = new List<Core.Models.Burger>();

        foreach (var b in infraBurgers)
        {
            string? imageBase64 = null;
            if (!string.IsNullOrWhiteSpace(b.ImageUrl))
            {
                imageBase64 = Base64Utils.GetBase64Image(b.ImageUrl);
            }
            coreBurgers.Add(new Core.Models.Burger
            {
                BurgerId = b.BurgerId,
                Name = b.Name,
                Description = b.Description,
                Price = b.Price,
                Stock = (int)(b.Stock - _burgerRepository.GetQuantityOfBurgerInOrders(b.BurgerId)),
                ImageUrl = imageBase64
            });
        }
        return coreBurgers;
    }

    public void AddBurger(Core.Models.Burger burger)
    {
        var infraBurger = new Burger
        {
            Name = burger.Name,
            Description = burger.Description,
            Price = burger.Price,
            Stock = burger.Stock,
        };
        _burgerRepository.AddBurger(infraBurger);
    }

    public void DeleteBurgerByName(string name)
    {

        _burgerRepository.DeleteBurgerByName(name);
    }

    public Core.Models.Burger? GetBurgerById(int burgerId)
    {
        var infraBurger = _burgerRepository.GetBurgerById(burgerId);
        if (infraBurger == null)
        {
            return null;
        }

        string? imageBase64 = null;
        if (!string.IsNullOrWhiteSpace(infraBurger.ImageUrl))
        {
            imageBase64 = Base64Utils.GetBase64Image(infraBurger.ImageUrl);
        }

        return new Core.Models.Burger
        {
            BurgerId = infraBurger.BurgerId,
            Name = infraBurger.Name,
            Description = infraBurger.Description,
            Price = infraBurger.Price,
            Stock = (int)(infraBurger.Stock - _burgerRepository.GetQuantityOfBurgerInOrders(infraBurger.BurgerId)),
            ImageUrl = imageBase64
        };
    }


}