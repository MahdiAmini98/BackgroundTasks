using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Domain.BackgroundTasks.MyQueue
{
    public class Producer
    {
        private Channel<string> _channel;

        public Producer(Channel<string> channel)
        {
            _channel = channel;
        }

        public Task SendMessage()
        {
            return Task.Run(async () =>
            {
                await Task.Delay(1500);
                for (int i = 0; i < 6; i++)
                {
                    await _channel.Writer.WriteAsync($"Message{i}");
                }
            });
        }
    }
}
