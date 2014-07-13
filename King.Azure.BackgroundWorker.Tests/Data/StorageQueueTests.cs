﻿namespace King.Azure.BackgroundWorker.Tests.Data
{
    using King.Azure.BackgroundWorker.Data;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    [TestFixture]
    public class StorageQueueTests
    {
        private const string ConnectionString = "UseDevelopmentStorage=true;";

        [Test]
        public void Constructor()
        {
            new StorageQueue("test", ConnectionString);
        }

        [Test]
        public void IQueue()
        {
            Assert.IsNotNull(new StorageQueue("test", ConnectionString) as IStorageQueue);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorTableNull()
        {
            new StorageQueue(null, ConnectionString);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorKeyNull()
        {
            new StorageQueue("test", null);
        }

        [Test]
        public void Name()
        {
            var name = Guid.NewGuid().ToString();
            var t = new StorageQueue(name, ConnectionString);
            Assert.AreEqual(name, t.Name);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task DeleteNull()
        {
            var name = Guid.NewGuid().ToString();
            var t = new StorageQueue(name, ConnectionString);
            await t.Delete(null);
        }
    }
}