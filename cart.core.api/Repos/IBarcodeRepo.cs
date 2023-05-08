using cart.core.api.Dtos;

namespace cart.core.api.Repos
{
    public interface IBarcodeRepo
    {
        bool PostBarcode(BarcodeDto info);
    }
}
