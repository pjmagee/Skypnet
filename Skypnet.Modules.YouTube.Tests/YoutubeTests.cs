// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YoutubeTests.cs" company="Patrick Magee">
//   Copyright © 2013
// </copyright>
// <summary>
//   The youtube tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Skypnet.Modules.YouTube.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// The youtube tests.
    /// </summary>
    [TestFixture]
    public class YoutubeTests
    {
        /// <summary>
        /// The Youtube provider.
        /// </summary>
        private IYouTubeProvider youTubeProvider;

        /// <summary>
        /// The test fixture set up.
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.youTubeProvider = new YouTubeProvider("AIzaSyC3diVUpQjg2niWpu84Be5hByOkg2-MsuU");
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
            string info = this.youTubeProvider.GetVideoInformation(id: "CSemARaqGqE");
            Assert.IsNotEmpty(info);
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
