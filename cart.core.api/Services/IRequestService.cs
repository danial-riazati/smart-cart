namespace cart.core.api.Services
{
    public interface IRequestService
    {
        Task<bool> PostDataAsync(string input);
        Task<bool> DeleteJsonObject(string id);
        Task<bool> DeleteJsonObject();

    }
}
