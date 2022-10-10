using cart.services.account_service.DataProvide;

namespace cart.services.account_service.Repos
{
    public class AccountRepo : IAccountRepo
    {
        private readonly CartDBContext _context;

        public AccountRepo(CartDBContext context)
        {
            _context = context;
        }

        public async Task<int> CheckIn(CheckInOut dto)
        {
            dto.CheckinDate = DateTime.Now;
            var model = await _context.AddAsync(dto);
            await SaveChanges();
            return model.Entity.Id;
            

        }

        public async Task SaveChanges()
        {
           await _context.SaveChangesAsync();
        }

        public  void SetCheckInFailed(int id)
        {

          var item =   _context.CheckInOuts.Where(x => x.Id == id).FirstOrDefault();
            if(item!=null)
            item.IsFailed = 1;

        }
    }
}
