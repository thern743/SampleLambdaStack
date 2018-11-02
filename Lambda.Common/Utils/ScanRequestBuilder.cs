using Amazon.DynamoDBv2.Model;
using Lambda.Common.Interfaces;

namespace Lambda.Common.Utils
{
    public class ScanRequestBuilder : IScanRequestBuilder
    {
        private readonly ScanRequest _request;

        public ScanRequestBuilder()
        {
            _request = new ScanRequest();
        }

        public IRequestBuilder SetTable(string tableName)
        {
            _request.TableName = tableName;
            return this;
        }

        public IRequestBuilder AddExpressionValue(string key, AttributeValue value)
        {
            _request.ExpressionAttributeValues.Add(key, value);
            return this;
        }

        public IRequestBuilder AddExpressionName(string key, string value)
        {
            _request.ExpressionAttributeNames.Add(key, value);
            return this;
        }

        public IRequestBuilder SetIndexName(string indexName)
        {
            _request.IndexName = indexName;
            return this;
        }

        public IRequestBuilder Top(int num)
        {
            _request.Limit = num;
            return this;
        }

        public IRequestBuilder StartKey(string key, AttributeValue value)
        {
            _request.ExclusiveStartKey.Add(key, value);
            return this;
        }

        public IRequestBuilder Select(string expression)
        {
            _request.ProjectionExpression = expression;
            return this;
        }

        public IRequestBuilder SetFilterExpression(string filter)
        {
            _request.FilterExpression = filter;
            return this;
        }

        public ScanRequest Build() => _request;
    }
}
