using System;
using System.Collections;
using System.Linq;
using System.Net;
using Amazon.Lambda.Core;
using Lambda.Common.Extensions;
using Lambda.Common.Interfaces;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace Lambda.Common.AWS.Lambda.Handlers
{
    public abstract class BaseApiController
    {
        public IDictionary EnvironmentVariables { get; set; }

        protected virtual IResponse Ok(string data)
        {
            return BuildResponse(HttpStatusCode.OK, data);
        }

        protected virtual IResponse Ok(object data)
        {
            return BuildResponse(HttpStatusCode.OK, data);
        }

        protected virtual IResponse Ok()
        {
            return BuildResponse(HttpStatusCode.OK);
        }

        protected virtual IResponse NoContent()
        {
            return BuildResponse(HttpStatusCode.NoContent);
        }

        protected virtual IResponse BadRequest(string data)
        {
            return BuildResponse(HttpStatusCode.BadRequest, data);
        }

        protected virtual IResponse BadRequest(object data)
        {
            return BuildResponse(HttpStatusCode.BadRequest, data);
        }

        protected virtual IResponse BadRequest()
        {
            return BuildResponse(HttpStatusCode.BadRequest);
        }

        protected virtual IResponse NotFound()
        {
            return BuildResponse(HttpStatusCode.NotFound);
        }

        protected virtual IResponse InternalServerError(object data)
        {
            return BuildResponse(HttpStatusCode.InternalServerError, data);
        }

        protected virtual IResponse InternalServerError()
        {
            return BuildResponse(HttpStatusCode.InternalServerError);
        }

        protected static string ParsePathParameter(IRequest request, string pathParameter)
        {
            return request.PathParameters.FirstOrDefault(x => x.Key.Equals(pathParameter, StringComparison.CurrentCultureIgnoreCase)).Value ?? string.Empty;
        }

        protected static string ParseHeader(IRequest request, string headerKey)
        {
            return request.Headers.FirstOrDefault(x => x.Key.Equals(headerKey, StringComparison.CurrentCultureIgnoreCase)).Value ?? string.Empty;
        }

        protected virtual IResponse ServiceUnavailable()
        {
            return BuildResponse(HttpStatusCode.ServiceUnavailable);
        }

        protected virtual IResponse ServiceUnavailable(object data)
        {
            return BuildResponse(HttpStatusCode.ServiceUnavailable, data);
        }

        protected bool IsSuccessfulStatusCode(HttpStatusCode statusCode)
        {
            return IsSuccessfulStatusCode((int) statusCode);
        }

        protected bool IsSuccessfulStatusCode(int statusCode)
        {
            return statusCode >= 200 && statusCode < 300;
        }

        private static IResponse BuildResponse(HttpStatusCode statusCode, object data)
        {
            return new Response(data?.ToJson(), statusCode);
        }

        private static IResponse BuildResponse(HttpStatusCode statusCode, string data)
        {
            return new Response(string.IsNullOrWhiteSpace(data) ? null : data, statusCode);
        }

        private static IResponse BuildResponse(HttpStatusCode statusCode)
        {
            return new Response(statusCode);
        }
    }
}
