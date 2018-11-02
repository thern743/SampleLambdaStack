using System;
using System.Threading.Tasks;
using Lambda.Common.AWS.Lambda.Handlers;
using Lambda.Common.Interfaces;
using Lambda.Routing;
using Newtonsoft.Json;
using SampleServerlessNetCoreLambda.Common;
using SampleServerlessNetCoreLambda.Services.SampleServiceTwo;

namespace SampleServerlessNetCoreLambda.Services.Controllers
{
    /// <summary>
    /// This is an example controller that handles an event message, such as from SQS. This should be treated as the entry point for orchestrating services.
    /// 
    /// The LambdaRoute attributes are loosely modeled after the WebAPI attributes and come from the Lambda.Routing library. The "default" value
    /// must be specified in cases where only one route exists.
    /// </summary>
    public class SampleEventController : BaseApiController
    {
        private readonly ISampleServiceTwo _service;

        public SampleEventController()
        {
            _service = new SampleServiceTwo.SampleServiceTwo();
        }

        [LambdaRoute("default")]
        public async Task<IResponse> ConsumeFooBar(MySampleEvent request, IContext context)
        {
            var msg = "Hello from ConsumeFooBar!";
            await _service.DoStuff(msg);

            foreach (var record in request.Records)
            {
                Console.WriteLine($"Processing Message: {record.MessageId}");
                await ProcessEventRecord(record);
            }

            return Ok(msg);
        }

        private async Task ProcessEventRecord(IEventMessage record)
        {
            if (record.Body == null) throw new System.ArgumentNullException(nameof(record.Body));
            var body = JsonConvert.DeserializeObject<string>(record.Body);

            if (!string.IsNullOrEmpty(body))
                await Task.Run(() => Console.WriteLine($"Doing work with {body}!"));
            else
                await Task.Run(() => Console.WriteLine("No work to do!"));
        }
    }
}
