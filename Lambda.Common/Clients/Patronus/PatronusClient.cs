using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using Lambda.Common.Clients.Patronus.Interfaces;
using Lambda.Common.Extensions;
using Lambda.Common.Interfaces;
using Lambda.Common.Utils;
using Newtonsoft.Json;

namespace Lambda.Common.Clients.Patronus
{
    public class PatronusClient : IPatronusClient
    {
        protected readonly IClientFactory<IDynamoClient> _dynamoClientFactory;

        protected string TableName { get; }

        public PatronusClient(IClientFactory<IDynamoClient> dynamoClientFactory, string tableName)
        {
            _dynamoClientFactory = dynamoClientFactory;
            TableName = tableName;
        }

        public async Task<IEnumerable<string>> GetPrimaryFromChd(string chd)
        {
            var client = _dynamoClientFactory.GetClient(TableName);
            var queryRequest = BuildIndexedQueryRequest("ChdAccountNumberGSI", "ChdAccountNumber", chd);
            var result = await client.QueryAsync(queryRequest);
            var chdUserTxs = result.Items.Select(i => i?["ChdUserTx"].S);
            return chdUserTxs;
        }

        public async Task<IEnumerable<string>> GetPrimaryFromDurableId(string durableId)
        {
            var client = _dynamoClientFactory.GetClient(TableName);
            var queryRequest = BuildIndexedQueryRequest("DurableGSI", "CustomerDurableId", durableId);
            var result = await client.QueryAsync(queryRequest);
            var chdUserTxs = result.Items.Select(i => i?["ChdUserTx"].S);
            return chdUserTxs;
        }

        public async Task<IPatronusRecord> GetRecord(string chdUserTx)
        {
            var client = _dynamoClientFactory.GetClient(TableName);
            var requestBuilder = new QueryRequestBuilder();
            
            var queryRequest = ((IQueryRequestBuilder)requestBuilder
                .SetKeyConditionExpression("ChdUserTx = :ChdUserTx")
                .SetTable(TableName)
                .AddExpressionValue(":ChdUserTx", new AttributeValue {S = chdUserTx}))
                .Build();

            var result = await client.QueryAsync(queryRequest);
            var record = result.Items.FirstOrDefault();
            return record == null 
                ? null 
                : JsonConvert.DeserializeObject<PatronusRecord>(record.ToJson());
        }

        private QueryRequest BuildIndexedQueryRequest(string indexName, string keyId, string value)
        {
            var requestBuilder = new QueryRequestBuilder();

            var queryRequest = ((IQueryRequestBuilder)requestBuilder
                .SetKeyConditionExpression($"{keyId} = :{keyId}")
                .SetTable(TableName)
                .SetIndexName(indexName)
                .AddExpressionValue($":{keyId}", new AttributeValue { S = value }))
                .ScanIndexForward()
                .Build();

            return queryRequest;
        }
    }
}