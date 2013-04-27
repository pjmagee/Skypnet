using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Skypnet.Modules.UrlShortener.Services.TinyUrl;

namespace Skypnet.Modules.UrlShortener.Tests
{
    [TestFixture]
    public class TinyUrlTests
    {
        private TinyUrlProvider tinyUrlProvider;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            tinyUrlProvider = new TinyUrlProvider();
        }

        [SetUp]
        public void SetUp()
        {
            
        }

        [Test(Description = "Testing tinyurl responds with a converted url")]
        public void Test()
        {
            string shorten = tinyUrlProvider.Shorten("http://google.com");
            Assert.AreEqual("http://tinyurl.com/2tx", shorten);
        }
        
        [TearDown]
        public void TearDown()
        {
            
        }
    }
}
