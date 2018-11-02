using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Lambda.Common.Clients.Patronus.Interfaces;

namespace Lambda.Common.Clients.Patronus
{
    public class DynamoClientWrapper : IDynamoClient
    {
        public IAmazonDynamoDB Client { get; set; }
        public Lazy<Table> DynamoTable => new Lazy<Table>(LoadTable);

        private string _tableName;

        public DynamoClientWrapper(IAmazonDynamoDB client, string tableName)
        {
            Client = client;
            _tableName = tableName;
        }

        public DynamoClientWrapper(IAmazonDynamoDB client)
        {
            Client = client;
        }

        public virtual Table LoadTable()
        {
            return Table.LoadTable(Client, _tableName);
        }

        public virtual Table LoadTable(string tableName)
        {
            _tableName = tableName;
            return LoadTable();
        }

        public string GetTableName() => DynamoTable.Value.TableName;

        public virtual Task<QueryResponse> QueryAsync(QueryRequest queryRequest) => this.Client.QueryAsync(queryRequest);

        public virtual Task<ScanResponse> ScanAsync(ScanRequest scanRequest) => this.Client.ScanAsync(scanRequest);

        public virtual Task<PutItemResponse> PutItemAsync(Document document)
        {
            var putItemRequest = new PutItemRequest
            {
                TableName = _tableName,
                Item = document.ToAttributeMap()
            };

            return this.Client.PutItemAsync(putItemRequest);
        }
            
    }
}