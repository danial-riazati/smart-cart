using cart.services.product_service.DataProvide;

namespace cart.services.product_service.Repos
{
    public interface IProductRepo
    {
        Task<string> UpdateProductSqlite(string sqliteversion);
        public bool CheckVersion(string sqliteversion);
        public string GetVersionUrl(string version);
        public void GetSmartCartinfos();
        public void AddVersion(string filename);
        public string GetLatestVersion();
    }
}
