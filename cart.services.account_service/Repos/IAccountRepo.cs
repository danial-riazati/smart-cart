using cart.services.account_service.DataProvide;
using cart.services.account_service.Models;

namespace cart.services.account_service.Repos
{
    public interface IAccountRepo
    {
        Task SaveChanges();
        Task<int> CheckIn(Checkin dto);
       

    }
}
