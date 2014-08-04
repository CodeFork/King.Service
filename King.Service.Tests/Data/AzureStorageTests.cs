﻿namespace King.Service.Tests.Data
{
    using King.Service.Data;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class AzureStorageTests
    {
        const string ConnectionString = "UseDevelopmentStorage=true";

        [Test]
        public void Constructor()
        {
            new AzureStorage(ConnectionString);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorNull()
        {
            new AzureStorage(null);
        }
    }
}