using System;
using cart.services.invoice_service.DataProvide;

namespace cart.services.invoice_service.Repos
{
    public class InvoiceRepo:IInvoiceRepo
    {
        private readonly InvoiceDBContext _context;

        public InvoiceRepo(InvoiceDBContext dBContext)
        {
            _context = dBContext;
        }

        public void GetAllInvoicesSortByAmount(int id)
        {
            throw new NotImplementedException();
        }

        public void GetAllInvoicesSortByDate(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Invoice?> GetInvoice(int id)
        {
            try
            {
                var invoice = _context.Invoices.Where(x => x.Id == id).FirstOrDefault();
                if (invoice == null)
                {
                    return null;
                }
                return invoice;
            }catch(Exception e)
            {
                return null;
            }

        }

         public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}

