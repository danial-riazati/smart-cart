using cart.core.api.Dtos;

namespace cart.core.api.Repos
{
    public interface IWeightRepo
    {
        bool PostWeight(WeightDto info);
        bool WeightAndBarcode(WeightDto info);
    }
}
