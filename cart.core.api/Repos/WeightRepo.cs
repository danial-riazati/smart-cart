
using cart.core.api.Dtos;
using cart.core.api.Services;
using NLog;
using Microsoft.Extensions.Configuration;
namespace cart.core.api.Repos;

using cart.core.api.Controllers;
using Microsoft.Extensions.Logging;

public class WeightRepo : IWeightRepo
{
    private static  Queue<WeightQueueDto> _weighQueue { get; } = new Queue<WeightQueueDto>();
    private readonly IBarcodeRepo _repository;
    private readonly CameraTcpServer _server;
    private readonly ILogger<WeightRepo> _logger;
    private readonly IRequestService _requestService;



    public WeightRepo(IRequestService requestService, ILogger<WeightRepo> logger, IBarcodeRepo repository)
    {
        _requestService = requestService;
        _logger = logger;
        _repository = repository;
        //_server = server;
      
    }

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
            _logger.LogInformation($"barcode dequeued is :{item.barcode}");
            if (item == null)
                item.barcode = "lock";
            Console.WriteLine($"barcode :{item.barcode}");
            item.time = DateTime.Now;
            HttpClient httpClient = new HttpClient();
            var configuration = new ConfigurationBuilder()
 .SetBasePath(Directory.GetCurrentDirectory())
 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
 .Build();
            //requestService = new RequestService(httpClient);
            if (info.isAdded == 1)
            {
                if (_requestService.PostDataAsync(item.barcode).Result)
                {
                    _logger.LogInformation($"post request sent");
                    Console.WriteLine($"post request sent");
                    return true;
                }
                   
                else return false;
               
            }else if (info.isAdded == 0)
            {
                if (_requestService.DeleteJsonObject().Result)
                {
                    _logger.LogInformation($"delete request sent");
                    Console.WriteLine($"delete request sent");
                    return true;
                }
                    
                else return false;
            }
            return false;
        }catch(Exception e)
        {
            _logger.LogError($"exception in weightRepo :{e.Message}");
            return false;
        }

    }
}
