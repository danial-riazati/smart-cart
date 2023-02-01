using System.Xml.Linq;

namespace cart.services.long_polling
{
    public class LongPolling
    {
        private static List<LongPolling> _sSubscribers = new List<LongPolling>();

        private TaskCompletionSource<bool> _TaskCompleteion = new TaskCompletionSource<bool>();
        private string _Channel { get; set; }
        private string _Message { get; set; }


        public LongPolling(string channel)
        {
            this._Channel = channel;
            lock (_sSubscribers)
            {
                _sSubscribers.Add(this);
            }
        }

        private void Notify(string message)
        {
            this._Message = message;
            this._TaskCompleteion.SetResult(true);
        }

        public async Task<string> WaitAsync()
        {
            await Task.WhenAny(_TaskCompleteion.Task, Task.Delay(20000));
            lock (_sSubscribers)
            {
                _sSubscribers.Remove(this);
            }
            return this._Message;
        }
        public static bool Publish(string channel, string message)
        {
            lock (_sSubscribers)
            {
                var all = _sSubscribers.ToList();
                foreach (var poll in all)
                {
                    if (poll._Channel == channel)
                    {
                        poll.Notify(message);
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
