using System;
using cart.services.invoice_service.DataProvide;

namespace cart.services.invoice_service.Repos
{
    public interface IInvoiceRepo
    {
        Task SaveChanges();
        Task<Invoice?> GetInvoice(int id);
        void GetAllInvoicesSortByDate(int id);
        void GetAllInvoicesSortByAmount(int id);

    }
}

