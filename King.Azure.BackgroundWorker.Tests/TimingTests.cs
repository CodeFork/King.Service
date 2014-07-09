﻿namespace King.Azure.BackgroundWorker.Tests
{

    using NUnit.Framework;
    using System;

    [TestFixture]
    public class TimingTests
    {
        [Test]
        public void Constructor()
        {
            new Timing();
        }

        [Test]
        public void IsITiming()
        {
            Assert.IsNotNull(new Timing() as ITiming);
        }

        [Test]
        public void AttemptMinimum()
        {
            var random = new Random();
            var min = random.Next();
            var time = new Timing();
            var ex = time.Exponential(0, 60, min);
            Assert.AreEqual(min, ex);
        }

        [Test]
        public void AttemptMaximmum()
        {
            var random = new Random();
            var max = random.Next(1, 60);
            var time = new Timing();
            var ex = time.Exponential((ulong)random.Next(61, 500), max);
            Assert.AreEqual(max, ex);
        }

        [Test]
        public void AttemptsSmall()
        {
            var random = new Random();
            var min = random.Next(1, 30);
            var max = random.Next(60, 120);

            var time = new Timing();
            for (ulong i = 0; i < 10; i++)
            {
                var ex = time.Exponential(i , max, min);

                Assert.AreEqual(2, ex);
            }
        }

        [Test]
        public void AttemptsLarge()
        {
            var random = new Random();
            var min = random.Next(3600, 4000);
            var max = random.Next(28800, 86400);

            var time = new Timing();
            for (ulong i = 0; i < 10; i++)
            {
                var ex = time.Exponential(i, max, min);

                Assert.AreEqual(2, ex);
            }
        }
    }
}