
using cart.core.api.Dtos;
using cart.core.api.Services;
using NLog;
using Microsoft.Extensions.Configuration;
namespace cart.core.api.Repos
{
    public class WeightRepo : IWeightRepo
    {
        private static  Queue<WeightQueueDto> _weighQueue { get; } = new Queue<WeightQueueDto>();
        private readonly BarcodeRepo _repository =new BarcodeRepo();
        private readonly CameraTcpServer _server;
        private  RequestService requestService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();


        //public WeightRepo(CameraTcpServer server)
        //{
        //    _server = server;
        ////  _repository = repository;
        //}

        public bool PostWeight(WeightDto info)
        {
            //string itemId=null;
            //if (info.isAdded == 1)
            //{
            //    WeighQueue.Enqueue(info);
            //     itemId = GetLastItemFromCameraQueue();
            //    HttpClient httpClient = new HttpClient();
            //    requestService = new RequestService(httpClient);
            //    if (requestService.PostDataAsync(itemId).Result)
            //        return true;
            //    else return false;

            //}
            //else if(info.isAdded==0)
            //{
            //    WeighQueue.Enqueue(info);
            //    //Thread.Sleep(5000);
            //    itemId = GetLastItemFromCameraQueue();
            //    HttpClient httpClient = new HttpClient();
            //    requestService = new RequestService(httpClient);
            //    if (requestService.DeleteJsonObject(itemId).Result)
            //        return true;
            //    else return false;
            //}
            //else 
            //    return false;
            return true;
            

        }
        public WeightQueueDto GetNextEvent()
        {
            return _weighQueue.Dequeue();
        }
        public BarcodeQueueDto GetNextEventFromQueue()
        {
            return _repository.GetNextEvent();
        }
        public string GetLastItemFromCameraQueue()
        {
            string itemId= _server.GetNextEvent();
            if(itemId==null)
            {
                return "lock";
            }else
                return itemId;
        }

        public bool WeightAndBarcode(WeightDto info)
        {
           
            try
            {
                WeightQueueDto weightQueueDto = new WeightQueueDto();
                weightQueueDto.time=DateTime.Now;
                weightQueueDto.weight=info.weight;
                _weighQueue.Enqueue(weightQueueDto);
                var barcodeQueue = _repository.getQueue();
                var item = barcodeQueue.Dequeue();
                _logger.Info($"barcode dequeued is :{item.barcode}");
                if (item == null)
                    item.barcode = "lock";
                Console.WriteLine($"barcode :{item.barcode}");
                item.time = DateTime.Now;
                HttpClient httpClient = new HttpClient();
                var configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
     .Build();
                requestService = new RequestService(httpClient,configuration);
                if (info.isAdded == 1)
                {
                    if (requestService.PostDataAsync(item.barcode).Result)
                    {
                        _logger.Info($"post request sent");
                        Console.WriteLine($"post request sent");
                        return true;
                    }
                       
                    else return false;
                   
                }else if (info.isAdded == 0)
                {
                    if (requestService.DeleteJsonObject().Result)
                    {
                        _logger.Info($"delete request sent");
                        Console.WriteLine($"delete request sent");
                        return true;
                    }
                        
                    else return false;
                }
                return false;
            }catch(Exception e)
            {
                _logger.Info($"exception in weightRepo :{e.Message}");
                return false;
            }

        }
    }
}
