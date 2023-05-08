
using cart.core.api.Dtos;
namespace cart.core.api.Repos
{
    public class WeightRepo : IWeightRepo
    {
        public Queue<WeightDto> WeighQueue { get; } = new Queue<WeightDto>();
        private readonly BarcodeRepo _repository;

        public WeightRepo(BarcodeRepo repository)
        {
            _repository = repository;
        }
        public bool PostWeight(WeightDto info)
        {
            WeighQueue.Enqueue(info);
           

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
    }
}
