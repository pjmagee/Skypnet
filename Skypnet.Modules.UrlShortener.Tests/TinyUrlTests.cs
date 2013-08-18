// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TinyUrlTests.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The tiny url tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.UrlShortener.Tests
{
    using NUnit.Framework;
    using Skypnet.Modules.UrlShortener.Services.TinyUrl;

    /// <summary>
    /// The tiny url tests.
    /// </summary>
    [TestFixture]
    public class TinyUrlTests
    {
        /// <summary>
        /// The tiny url provider.
        /// </summary>
        private TinyUrlProvider tinyUrlProvider;

        /// <summary>
        /// The test fixture set up.
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.tinyUrlProvider = new TinyUrlProvider();
        }

        /// <summary>
        /// The set up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
        }

        /// <summary>
        /// The test.
        /// </summary>
        [Test(Description = "Testing tinyurl responds with a converted url")]
        public void Test()
        {
            string shorten = this.tinyUrlProvider.Shorten("http://google.com");
            Assert.AreEqual("http://tinyurl.com/2tx", shorten);
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
        }
    }
}
