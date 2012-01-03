using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntroToAsync
{
    public class SlowPoke
    {
        #region Synchronous
        public int Work()
        {
            Thread.Sleep(3000);
            return 42;
        }
        #endregion

        #region Asynchronous old style
        public IAsyncResult BeginWork(AsyncCallback callback, object state)
        {
            var result = new SlowPokeAsyncResult(callback, state);
            Task.Factory.StartNew(delegate { result.Run(); });
            return result;
        }

        public int EndWork(IAsyncResult result)
        {
            return ((SlowPokeAsyncResult)result).Answer;
        }
        #endregion

        #region Asynchronous new style
        public async Task<int> WorkAsync()
        {
            await Task.Run(delegate
            {
                Thread.Sleep(3000);
            });
            return 42;
        }
        #endregion
    }
}
