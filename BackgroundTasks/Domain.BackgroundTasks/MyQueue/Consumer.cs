using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Domain.BackgroundTasks.MyQueue
{
    public class Consumer
    {
        private Channel<string> _channel;
        public Consumer(Channel<string> channel)
        {
            _channel = channel;
        }

        public Task ReceviceMessage()
        {
            return Task.Run(async () =>
            {
                while (await _channel.Reader.WaitToReadAsync())
                {
                    string message = await _channel.Reader.ReadAsync();
                   //Show Message -----> _logger.LogInformation($"{message}");
                }
            });
        }
    }
}
