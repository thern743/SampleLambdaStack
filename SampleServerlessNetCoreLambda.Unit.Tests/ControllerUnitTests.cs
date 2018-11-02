using NUnit.Framework;
using SampleServerlessNetCoreLambda.Services.Controllers;

namespace SampleServerlessNetCoreLambda.Unit.Tests
{
    [TestFixture]
    public class ControllerUnitTests
    {
        [Test]
        public void SampleRequestController_Test()
        {
            var controller = new SampleRequestController();
            Assert.IsNotNull(controller);
        }

        [Test]
        public void SampleEventController_Test()
        {
            var controller = new SampleEventController();
            Assert.IsNotNull(controller);
        }
    }
}
