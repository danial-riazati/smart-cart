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

        public async Task<string> GenerateProductSqlight()
        {
            try
            {
                
                var product = _context.Products.ToList();
                var storeDbversion = smartCartinfo.Where(b => b.DataName.Equals("StoreDbVersion")).FirstOrDefault().DataValue;
                string fileName = "Product" + storeDbversion + ".sqlite";
                File.Delete(Path.Combine(Directory.GetCurrentDirectory(), fileName));
                string connectionString = "Data Source="+ fileName + ";Version=3;";
                SQLiteConnection connection = new SQLiteConnection(connectionString);
                connection.Open();
                string createTableQuery = "CREATE TABLE IF NOT EXISTS Product (Id INTEGER PRIMARY KEY , Name TEXT, Description TEXT , Price TEXT , ImageUrl TEXT , Barcode TEXT)";
                SQLiteCommand createcommand = new SQLiteCommand(createTableQuery, connection);
                createcommand.ExecuteNonQuery();
                foreach (var item in product)
                {
                    using (var insertCommand = new SQLiteCommand("INSERT INTO Product (Id , Name , Description , Price , ImageUrl , Barcode) VALUES (@id ,@name , @description ,  @price , @imageUrl , @barcode) ", connection))
                    {
                        insertCommand.Parameters.AddRange(new[] {
                                     new SQLiteParameter("@id", item.Id),
                                     new SQLiteParameter("@name", item.Name),
                                     new SQLiteParameter("@description", item.Description),
                                     new SQLiteParameter("@price", item.Price),
                                     new SQLiteParameter("@imageUrl", item.ImageUrl),
                                     new SQLiteParameter("@barcode", item.Barcode),
                        });
                        insertCommand.ExecuteNonQuery();
                    }
                }
                string sourceFile = Path.Combine(Directory.GetCurrentDirectory(), fileName); 
                string destinationFile = Path.Combine(smartCartinfo.Where(b => b.DataName.Equals("VersionFolderUrl")).FirstOrDefault().DataValue , fileName) ;
                File.Copy(sourceFile, destinationFile, true);
                connection.Close(); 
                //File.Delete(sourceFile);


                _context.SmartCartinfos.Where(b => b.DataName.Equals("SqlightVersion")).FirstOrDefault().DataValue = storeDbversion;
                _context.SaveChanges();
                GetSmartCartinfos();
                AddVersion(fileName);
                return "done";



            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
        public HttpResponseMessage DownloadFile(string filePath)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            byte[] fileContent = File.ReadAllBytes(filePath);

            response.Content = new ByteArrayContent(fileContent);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = Path.GetFileName(filePath)
            };

            return response;
        }
        public string GetsqlightVersion() => _context.SmartCartinfos.Where(a => a.DataName.Equals("SqlightVersion")).FirstOrDefault().DataValue;
        public string GetStoreDbVersion() => _context.SmartCartinfos.Where(a => a.DataName.Equals("StoreDbVersion")).FirstOrDefault().DataValue;
        public string GetVersionUrl(string version)
        {
            var name = "product" + version;
            return _context.Versions.Where(a=>a.VersionName.Equals(name)).FirstOrDefault().Url;
        } 
        public bool CheckVersion()
        {
            var sqlightversion = _context.SmartCartinfos.Where(a => a.DataName.Equals("SqlightVersion")).FirstOrDefault().DataValue;
            var storeDbversion = _context.SmartCartinfos.Where(b => b.DataName.Equals("StoreDbVersion")).FirstOrDefault().DataValue;
            return sqlightversion.Equals(storeDbversion);
        }
        public void AddVersion(string filename)
        {
            var versionNumber = filename.Substring(7);
            var version = new DataProvide.Version(filename,versionNumber , DateTime.Now, smartCartinfo.Where(b => b.DataName.Equals("VersionFolderUrl")).FirstOrDefault().DataValue);
            _context.Versions.Add(version);
            _context.SaveChanges();
        }

    }
}
