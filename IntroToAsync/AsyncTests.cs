using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntroToAsync
{
    [TestClass]
    public class AsyncTests
    {
        private SlowPoke _slowPoke;

        [TestInitialize]
        public void Initialize()
        {
            _slowPoke = new SlowPoke();
        }

        [TestMethod]        
        public void CallMethodSynchronously()
        {
            int answer = _slowPoke.Work();
            Assert.AreEqual(42, answer);
        }

        [TestMethod]
        public void CallMethodAsynchronouslyOldStyle()
        {
            IAsyncResult r = _slowPoke.BeginWork(delegate(IAsyncResult result)
            {
                int answer = _slowPoke.EndWork(result);
                Assert.AreEqual(42, answer);
            }, null);

            r.AsyncWaitHandle.WaitOne();
        }

        [TestMethod]
        public async Task CallMethodAsynchronouslyNewStyle()
        {
            Task<int> task = _slowPoke.WorkAsync();
            int answer = await task;
            Assert.AreEqual(42, answer);
        }
    }
}
