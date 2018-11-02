using NUnit.Framework;
using SampleServerlessNetCoreLambda.Services.SampleServiceOne;
using SampleServerlessNetCoreLambda.Services.SampleServiceTwo;

namespace SampleServerlessNetCoreLambda.Unit.Tests
{
    [TestFixture]
    public class ServiceUnitTests

    {
        [Test]
        public void SampleServiceOne_Test()
        {
            var service = new SampleServiceOne();
            Assert.IsNotNull(service);
        }

        [Test]
        public void SampleServiceTwo_Test()
        {
            var service = new SampleServiceTwo();
            Assert.IsNotNull(service);
        }
    }
}
