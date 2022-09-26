using System;
using cart.services.account_service.DataProvide;

namespace cart.services.cart_service.Repos
{
    public interface ICartRepo
    {
        Task SaveChanges();
        Task AddCart(Cart cart);
        Task UpdateCart(Cart cart);
        void DeleteCart(int id);
        
    }
}

