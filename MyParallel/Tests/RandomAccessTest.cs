using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;


namespace Tests
{
    [TestClass]
    public class RandomAccessTest
    {
        [TestMethod]
        public void TestRandomAccessSpeed()
        {
            Stopwatch swSeq = new Stopwatch();
            Stopwatch swRandom = new Stopwatch();

            byte[] arr = new byte[10000000];
            int x = 0;
            for(int i = 0; i < arr.Length; i++)
            {
                swSeq.Start();
                x = arr[i];
                swSeq.Stop();
            }

            Random rnd = new Random();
            for(int i = 0; i < arr.Length; i++)
            {               
                int index = rnd.Next(arr.Length);
                swRandom.Start();
                x = arr[index];
                swRandom.Stop();
            }
            Assert.IsTrue(swSeq.ElapsedMilliseconds < swRandom.ElapsedMilliseconds);
        }
    }
}
