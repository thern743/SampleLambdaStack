using System.Collections.Generic;
using System.Net;
using Lambda.Common.Interfaces;

namespace Lambda.Common.AWS.SQS
{
    public class SqsSendMessageResponse : IHttpMessageResponse
    {
        public string MessageId { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public IDictionary<string, string> Links { get; set; }
    }
}
