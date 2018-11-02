using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;
using Lambda.Common.Interfaces;

namespace Lambda.Common.Utils
{
    public class QueryRequestBuilder : IQueryRequestBuilder
    {
        private readonly QueryRequest _request;

        public QueryRequestBuilder()
        {
            _request = new QueryRequest { ExpressionAttributeValues = new Dictionary<string, AttributeValue>() };
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

        public IRequestBuilder SetFilterExpression(string filter)
        {
            _request.FilterExpression = filter;
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

        public IQueryRequestBuilder ScanIndexForward(bool enable = true)
        {
            _request.ScanIndexForward = enable;
            return this;
        }

        public IQueryRequestBuilder SetKeyConditionExpression(string conditionExpression)
        {
            _request.KeyConditionExpression = conditionExpression;
            return this;
        }

        public QueryRequest Build() => _request;
    }
}
