namespace cart.services.payment_service.Repos
{
    public interface IPaymentRepo
    {
        Task<string?> GetPaymentUrl(int id);
        Task<string?> VerifyPayment(int id);
    }
}