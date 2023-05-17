using cart.core.api.DataProvide;
using cart.core.api.Dtos;
using cart.core.api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;


namespace cart.core.api.Repos
{
    public class BarcodeRepo : IBarcodeRepo
    {
        private static Queue<BarcodeQueueDto> _barcodeQueue  = new Queue<BarcodeQueueDto>();
        private readonly ProductDbContext _context;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private RequestService requestService;

        public async Task<bool>  PostBarcode(BarcodeDto info)
        {
         
            BarcodeQueueDto barcodeQueueDto= new BarcodeQueueDto();
            barcodeQueueDto.time = DateTime.Now;
            barcodeQueueDto.barcode = info.barcode;
            _barcodeQueue.Enqueue(barcodeQueueDto);
            Console.WriteLine("barcode added in the queue");
            _logger.Info("barcode added in the queue");
            HttpClient httpClient = new HttpClient();
            var configuration = new ConfigurationBuilder()
 .SetBasePath(Directory.GetCurrentDirectory())
 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
 .Build();
            requestService = new RequestService(httpClient, configuration);
            if (requestService.PostDataAsync(info.barcode).Result)
            {
                _logger.Info($"post request sent");
                Console.WriteLine($"post request sent");
                return true;
            }

            // BarcodeQueueDto[] arr = _barcodeQueue.ToArray();
            return true;
        }
        public BarcodeQueueDto GetNextEvent()
        {
            return _barcodeQueue.Dequeue();
        }
        public Queue<BarcodeQueueDto> getQueue()
        {
            return _barcodeQueue;
        }
    }
}
