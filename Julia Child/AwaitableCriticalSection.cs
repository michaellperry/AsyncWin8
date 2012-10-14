/***************************************************************
 * AwaitableCriticalSection
 * 
 * Michael L Perry
 * http://qedcode.com
 * 
 * Protect shared resources (such as files) within asynchronous
 * methods without blocking the thread.
 * 
 * Only use this code if you understand how it works.
 * 
 * Copyright (C) 2012 Michael L Perry
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in
 * the Software without restriction, including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is furnished to do
 * so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 ***************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QEDCode
{
    public class AwaitableCriticalSection
    {
        private class Token : IAsyncResult
        {
            private ManualResetEvent _event = new ManualResetEvent(false);
            private bool _synchronous = false;

            public object AsyncState
            {
                get { return null; }
            }

            public WaitHandle AsyncWaitHandle
            {
                get { return _event; }
            }

            public bool CompletedSynchronously
            {
                get { return _synchronous; }
            }

            public bool IsCompleted
            {
                get { return _event.WaitOne(TimeSpan.Zero); }
            }

            public void Signal(bool synchronous)
            {
                _synchronous = synchronous;
                _event.Set();
            }
        }

        private class Disposable : IDisposable
        {
            private readonly AwaitableCriticalSection _owner;

            public Disposable(AwaitableCriticalSection owner)
            {
                _owner = owner;
            }

            public void Dispose()
            {
                _owner.Exit();
            }
        }

        private Queue<Token> _tokens = new Queue<Token>();
        private bool _busy = false;
        private IDisposable _disposable;

        public AwaitableCriticalSection()
        {
            _disposable = new Disposable(this);
        }

        public Task<IDisposable> EnterAsync()
        {
            lock (this)
            {
                Token token = new Token();
                _tokens.Enqueue(token);
                if (!_busy)
                {
                    _busy = true;
                    _tokens.Dequeue().Signal(true);
                }
                return Task.Factory.FromAsync(token, result => _disposable);
            }
        }

        private void Exit()
        {
            lock (this)
            {
                if (_tokens.Any())
                {
                    _tokens.Dequeue().Signal(false);
                }
                else
                {
                    _busy = false;
                }
            }
        }
    }
}
