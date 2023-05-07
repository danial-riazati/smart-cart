namespace cart.core.api.Repos
{
    public interface IWeightRepo
    {
        Task<bool> RecieveWeightData(string Weight);
    }
}
