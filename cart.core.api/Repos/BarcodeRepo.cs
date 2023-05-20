using cart.core.api.DataProvide;
using cart.core.api.Dtos;
using cart.core.api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Microsoft.Extensions.Logging;


namespace cart.core.api.Repos
{
    public class BarcodeRepo : IBarcodeRepo
    {
        private static Queue<BarcodeQueueDto> _barcodeQueue  = new Queue<BarcodeQueueDto>();
        private readonly ProductDbContext _context;
        private readonly ILogger<BarcodeRepo> _logger;
        private readonly IRequestService _requestService;

        public BarcodeRepo(IRequestService requestService, ILogger<BarcodeRepo> logger)
        {
            _requestService = requestService;
            _logger = logger;
        }

        public async Task<bool>  PostBarcode(BarcodeDto info)
        {
         
            BarcodeQueueDto barcodeQueueDto= new BarcodeQueueDto();
            barcodeQueueDto.time = DateTime.Now;
            barcodeQueueDto.barcode = info.barcode;
            _barcodeQueue.Enqueue(barcodeQueueDto);
            Console.WriteLine("barcode added in the queue");
            _logger.LogInformation("barcode added in the queue");
            //HttpClient httpClient = new HttpClient();
            
            //requestService = new RequestService(httpClient);
           // _requestService.re
            if (_requestService.PostDataAsync(info.barcode).Result)
            {
                _logger.LogInformation($"post request sent");
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
