using cart.services.product_service.DataProvide;

namespace cart.services.product_service.Repos
{
    public interface IProductRepo
    {
        Task<string> GenerateProductSqlight();
        public HttpResponseMessage DownloadFile(string filePath);
        public string GetStoreDbVersion();
        public string GetsqlightVersion();
        public bool CheckVersion();
        public string GetVersionUrl(string version);
        public void GetSmartCartinfos();
        public void AddVersion(string filename);
    }
}
