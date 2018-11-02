using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Lambda.Common.AWS.Lambda.Handlers;
using Lambda.Common.Interfaces;
using Lambda.Routing;
using Lambda.Routing.HttpVerbAttributes;
using SampleServerlessNetCoreLambda.Services.SampleServiceOne;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace SampleServerlessNetCoreLambda.Services.Controllers
{
    /// <summary>
    /// This is an example controller that handles an API request such as one from API Gateway. This should be treated as the entry point for orchestrating services.
    /// 
    /// The HttpPost, HttpGet, and LambdaRoute attributes are loosely modeled after the WebAPI attributes and come from the Lambda.Routing library.
    /// </summary>
    public class SampleRequestController : BaseApiController
    {
        private readonly ISampleServiceOne _service;

        public SampleRequestController()
        {
            _service = new SampleServiceOne.SampleServiceOne();
        }

        [HttpPost]
        [LambdaRoute("/foo/bar/{value}")]
        public async Task<IResponse> PostFooBar(IRequest request, IContext context)
        {
            var value = ParsePathParameter(request, "value");
            var msg = $"Hello from PostFooBar with {value}!";
            await _service.DoStuff(msg);
            return Ok(msg);
        }

        [HttpPost]
        [LambdaRoute("/foo/bar/baz/{value}")]
        public async Task<IResponse> PostFooBarBaz(IRequest request, IContext context)
        {
            var value = ParsePathParameter(request, "value");
            var msg = $"Hello from PostFooBarBaz with {value}!";
            await _service.DoStuff(msg);
            return Ok(msg);
        }

        [HttpGet]
        [LambdaRoute("/foo/bar/{value}")]
        public async Task<IResponse> GetFooBar(IRequest request, IContext context)
        {
            var value = ParsePathParameter(request, "value");
            var msg = $"Hello from GetFooBar with {value}!";
            await _service.DoStuff(msg);
            return Ok(msg);
        }

        [HttpGet]
        [LambdaRoute("/foo/bar/baz/{value}")]
        public async Task<IResponse> GetFooBarBaz(IRequest request, IContext context)
        {
            var value = ParsePathParameter(request, "value");
            var msg = $"Hello from GetFooBarBaz with {value}!";
            await _service.DoStuff(msg);
            return Ok(msg);
        }
    }
}
