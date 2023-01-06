using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Domain.BackgroundTasks.MyQueue
{
    public class MyQueue
    {
        public Channel<string> _channel;

        public MyQueue()
        {
            _channel = Channel.CreateUnbounded<string>();
        }
    }
}
