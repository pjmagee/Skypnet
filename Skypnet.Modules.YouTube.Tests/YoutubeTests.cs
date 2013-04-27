using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Skypnet.Modules.YouTube.Tests
{
    [TestFixture]
    public class YoutubeTests
    {
        private IYouTubeProvider youTubeProvider;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            youTubeProvider = new YouTubeProvider();
            youTubeProvider.ApiKey = "AIzaSyC3diVUpQjg2niWpu84Be5hByOkg2-MsuU";
        }

        [SetUp]
        public void SetUp()
        {

        }

        [Test(Description = "Testing tinyurl responds with a converted url")]
        public void Test()
        {
            string info = youTubeProvider.GetVideoInformation(id: "CSemARaqGqE");
            Assert.IsNotEmpty(info);
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}
