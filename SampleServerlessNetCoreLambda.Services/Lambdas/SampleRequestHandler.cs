using Lambda.Common.AWS.Lambda.Handlers;
using SampleServerlessNetCoreLambda.Services.Controllers;

namespace SampleServerlessNetCoreLambda.Services.Lambdas
{
    /// <summary>
    /// This is a sample Lambda "handler". It allows a degree of separation by passing through to the controller so that
    /// the YAML configuration can be configured to allow multiple functions for a single handler.
    /// 
    /// It is in this way that there is a decoupling between the controllers and services that actually perform the work,
    /// and having to deploy to AWS Lambda. For instance, to deploy to another cloud provider, this class can simply be removed
    /// and the underlying controller class can be converted to be used by the new cloud provider or container service.
    /// </summary>
    public class SampleRequestHandler : BaseLambdaRequestHandler<SampleRequestController>
    {
    }
}
