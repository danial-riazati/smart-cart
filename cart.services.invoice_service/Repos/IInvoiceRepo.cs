using System;
using cart.services.invoice_service.DataProvide;
using cart.services.invoice_service.Model;

namespace cart.services.invoice_service.Repos
{
    public interface IInvoiceRepo
    {
        Task SaveChanges();
        Task<Invoice?> GetInvoice(int id);
        Task SaveInvoice(InvoiceDTO dto);

        List<Invoice> GetAllInvoicesSortByDate();
        List<Invoice> GetAllInvoicesSortByAmount();

    }
}

