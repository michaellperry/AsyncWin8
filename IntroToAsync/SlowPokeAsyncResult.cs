using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IntroToAsync
{
    public class SlowPokeAsyncResult : IAsyncResult
    {
        private AsyncCallback _callback;
        private object _state;
        private ManualResetEvent _event;
        private int _answer;

        public SlowPokeAsyncResult(AsyncCallback callback, object state)
        {
            _callback = callback;
            _state = state;
            _event = new ManualResetEvent(false);
        }

        public object AsyncState
        {
            get { return _state; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get { return _event; }
        }

        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public bool IsCompleted
        {
            get { return _event.WaitOne(0); }
        }

        public void Run()
        {
            Thread.Sleep(3000);
            _answer = 42;
            _event.Set();
            _callback(this);
        }

        public int Answer
        {
            get
            {
                _event.WaitOne();
                return _answer;
            }
        }
    }
}
