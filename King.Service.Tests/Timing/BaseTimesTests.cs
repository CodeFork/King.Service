﻿namespace King.Azure.BackgroundWorker.Tests.Timing
{
    using King.Service.Timing;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class BaseTimesTests
    {
        [Test]
        public void NoRepeat()
        {
            Assert.AreEqual(-1, BaseTimes.NoRepeat);
        }
                
        [Test]
        public void MinimumTiming()
        {
            Assert.AreEqual(10, BaseTimes.MinimumTiming);
        }
        
        [Test]
        public void MaximumTiming()
        {
            Assert.AreEqual(180, BaseTimes.MaximumTiming);
        }
        
        [Test]
        public void MinimumStorageTiming()
        {
            Assert.AreEqual(15, BaseTimes.MinimumStorageTiming);
        }
        
        [Test]
        public void MaximumStorageTiming()
        {
            Assert.AreEqual(180, BaseTimes.MaximumStorageTiming);
        }
    }
}