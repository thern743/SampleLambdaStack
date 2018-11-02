using Amazon.DynamoDBv2.Model;

namespace Lambda.Common.Interfaces
{
    public interface IRequestBuilder
    {
        IRequestBuilder SetTable(string tableName);
        IRequestBuilder AddExpressionValue(string key, AttributeValue value);
        IRequestBuilder AddExpressionName(string key, string value);
        IRequestBuilder SetIndexName(string indexName);
        IRequestBuilder Select(string expression);
        IRequestBuilder StartKey(string key, AttributeValue value);
        IRequestBuilder Top(int num);
        IRequestBuilder SetFilterExpression(string filter);
    }
}