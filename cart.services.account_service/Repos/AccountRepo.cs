using cart.services.account_service.DataProvide;

namespace cart.services.account_service.Repos
{
    public class AccountRepo : IAccountRepo
    {
        private readonly AccountDBContext _context;

        public AccountRepo(AccountDBContext context)
        {
            _context = context;
        }

        public async Task<int> CheckIn(Checkin dto)
        {
            dto.Date = DateTime.Now;
            var model = await _context.AddAsync(dto);
            await SaveChanges();
            return model.Entity.Id;


        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}
