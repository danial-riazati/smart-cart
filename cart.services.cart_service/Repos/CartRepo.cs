using System;
using cart.services.account_service.DataProvide;

namespace cart.services.cart_service.Repos
{
    public class CartRepo:ICartRepo
    {
        private readonly CartDBContext _context;

        public CartRepo(CartDBContext context)
        {
            _context = context;
        }

        public Task AddCart(Cart cart)
        {
            _context.Carts.Add(cart);
            throw new NotImplementedException();
        }

        public void DeleteCart(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task UpdateCart(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}

