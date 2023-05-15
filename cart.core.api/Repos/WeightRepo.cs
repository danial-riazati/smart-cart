﻿
using cart.core.api.Dtos;
using cart.core.api.Services;

namespace cart.core.api.Repos
{
    public class WeightRepo : IWeightRepo
    {
        private static  Queue<WeightQueueDto> _weighQueue { get; } = new Queue<WeightQueueDto>();
        private readonly BarcodeRepo _repository =new BarcodeRepo();
        private readonly CameraTcpServer _server;
        private  RequestService requestService;

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
                if (item == null)
                    item.barcode = "lock";
                item.time = DateTime.Now;
                HttpClient httpClient = new HttpClient();
                requestService = new RequestService(httpClient);
                return true;
            }catch(Exception e)
            {
                return false;
            }
         

            


        }
    }
}
