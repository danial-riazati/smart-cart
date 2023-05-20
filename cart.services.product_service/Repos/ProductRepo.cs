using System.Linq;
using cart.services.product_service.DataProvide;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using System.Net;
using System.Net.Http.Headers;



namespace cart.services.product_service.Repos
{
    public class ProductRepo : IProductRepo
    {
        private readonly ProductDBContext _context;
        public static List<SmartCartinfo> smartCartinfo;

        public ProductRepo(ProductDBContext dBContext)
        {
            _context = dBContext;
            smartCartinfo = _context.SmartCartinfos.ToList();
        }
        public void GetSmartCartinfos()
        {
            smartCartinfo = _context.SmartCartinfos.ToList();
        }

        public async Task<string> UpdateProductSqlite(string sqliteversion)
        {
            try
            {
                var product = _context.Items.ToList();
                var storeDbversion = smartCartinfo.Where(b => b.DataName.Equals("StoreDbVersion")).FirstOrDefault().DataValue;

                string fileName = "Item" + storeDbversion + ".sqlite";
                string destinationFile = Path.Combine(smartCartinfo.Where(b => b.DataName.Equals("VersionFolderUrl")).FirstOrDefault().DataValue, fileName);
                if (System.IO.File.Exists(destinationFile))
                    File.Delete(destinationFile);
                string connectionString = "Data Source=" + destinationFile + ";Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string createTableQuery = "CREATE TABLE IF NOT EXISTS Item (id TEXT PRIMARY KEY , name TEXT, description TEXT , price TEXT , bytes TEXT )";
                    SQLiteCommand createcommand = new SQLiteCommand(createTableQuery, connection);
                    createcommand.ExecuteNonQuery();
                    createcommand.Dispose();
                    foreach (var item in product)
                    {
                        using (var insertCommand = new SQLiteCommand("INSERT INTO Item (id , name , description , price , bytes) VALUES (@id ,@name , @description ,  @price , @bytes ) ", connection))
                        {
                            insertCommand.Parameters.AddRange(new[] {
                                     new SQLiteParameter("@id", item.Id),
                                     new SQLiteParameter("@name", item.Name),
                                     new SQLiteParameter("@description", item.Description),
                                     new SQLiteParameter("@price", item.Price),
                                     new SQLiteParameter("@bytes", item.Bytes),                                     
                        });
                            insertCommand.ExecuteNonQuery();
                            insertCommand.Dispose();
                        }
                    }
                    connection.Close();
                }
                AddVersion(fileName);
                sqliteversion = storeDbversion;
                return sqliteversion;

            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
        public string GetVersionUrl(string version) => _context.Versions.Where(a => a.VersionName.Equals("Item" + version + ".sqlite")).FirstOrDefault().Url;
        
        public bool CheckVersion(string sqliteversion) => sqliteversion.Equals(smartCartinfo.Where(b => b.DataName.Equals("StoreDbVersion")).FirstOrDefault().DataValue);

        public string GetLatestVersion() => _context.Versions.OrderByDescending(a => a.VersionNumber).FirstOrDefault().VersionNumber;

        public void AddVersion(string filename)
        {
            var allversions = _context.Versions.Select(a => a.VersionNumber).ToList();
            var versionNumber = filename.Substring(7).Replace(".sqlite", "");
            if (!allversions.Contains(versionNumber))
            {
                var version = new DataProvide.Version(filename, versionNumber, DateTime.Now, smartCartinfo.Where(b => b.DataName.Equals("VersionFolderUrl")).FirstOrDefault().DataValue + "\\" + filename);
                _context.Versions.Add(version);
                _context.SaveChanges();
            }
        }
    }
}
