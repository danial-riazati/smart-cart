
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
            WeighQueue.Enqueue(info);
            var itemId=GetLastItemFromCameraQueue();
            HttpClient httpClient = new HttpClient();
            requestService = new RequestService(httpClient);
            //string itemId = _server.GetNextEvent();
            return true;
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
            return _server.GetNextEvent();
        }
    }
}
