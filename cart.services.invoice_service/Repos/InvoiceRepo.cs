using System;
using cart.services.invoice_service.DataProvide;
using cart.services.invoice_service.Model;
using Mapster;
using Microsoft.EntityFrameworkCore;
using MoreLinq;

namespace cart.services.invoice_service.Repos
{
    public class InvoiceRepo : IInvoiceRepo
    {
        private readonly InvoiceDBContext _context;

        public InvoiceRepo(InvoiceDBContext dBContext)
        {
            _context = dBContext;
        }
        public async Task<Invoice?> GetInvoice(int id)
        {
            try
            {
                var invoice = _context.Invoices.Where(x => x.InvoiceId == id).FirstOrDefault();
                if (invoice == null)
                {
                    return null;
                }
                return invoice;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task SaveInvoice(InvoiceDTO dto)
        {
            try
            {
                var invoice = dto.Adapt<Invoice>();
                _context.Add(invoice);
                await SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var errorMessages = ex.InnerException?.InnerException?.Message;
                throw new Exception($"Error saving data to database: {errorMessages}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving data to database", ex);
            }

        }
        public List<Invoice> GetAllInvoicesSortByAmount()
        {
            return _context.Invoices.OrderBy(a => a.TotalAmount).ToList();
        }

        public List<Invoice> GetAllInvoicesSortByDate()
        {
            return _context.Invoices.OrderBy(a => a.Date).ToList();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}

