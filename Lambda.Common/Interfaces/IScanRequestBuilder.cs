using Amazon.DynamoDBv2.Model;

namespace Lambda.Common.Interfaces
{
    public interface IScanRequestBuilder : IRequestBuilder
    {
        ScanRequest Build();
    }
}