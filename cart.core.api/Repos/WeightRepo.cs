
using cart.core.api.Dtos;
using cart.core.api.Services;

namespace cart.core.api.Repos
{
    public class WeightRepo : IWeightRepo
    {
        public Queue<WeightDto> WeighQueue { get; } = new Queue<WeightDto>();
        private readonly BarcodeRepo _repository;
        private readonly CameraTcpServer _server;
        private  RequestService requestService;

        public WeightRepo(CameraTcpServer server)
        {
            _server = server;
          //  _repository = repository;
        }
        public bool PostWeight(WeightDto info)
        {
            string itemId=null;
            if (info.isAdded == 1)
            {
                WeighQueue.Enqueue(info);
                 itemId = GetLastItemFromCameraQueue();
              
            }
            else if(info.isAdded==0)
            {
                WeighQueue.Enqueue(info);
                Thread.Sleep(5000);
                itemId = GetLastItemFromCameraQueue(); 
            }
            else 
                return false;
            HttpClient httpClient = new HttpClient();
            requestService = new RequestService(httpClient);
            if (requestService.PostDataAsync(itemId).Result)
                return true;
            else return false;

        }
        public WeightDto GetNextEvent()
        {
            return WeighQueue.Dequeue();
        }
        public BarcodeDto GetNextEventFromQueue()
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
    }
}
