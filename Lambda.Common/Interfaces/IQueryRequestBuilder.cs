using Amazon.DynamoDBv2.Model;

namespace Lambda.Common.Interfaces
{
    public interface IQueryRequestBuilder : IRequestBuilder
    {
        IQueryRequestBuilder SetKeyConditionExpression(string conditionExpression);
        IQueryRequestBuilder ScanIndexForward(bool enable = true);
        QueryRequest Build();
    }
}