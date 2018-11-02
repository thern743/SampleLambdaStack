using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace Lambda.Common.Clients.Patronus.Interfaces
{
    public interface IDynamoClient
    {
        Table LoadTable();
        Table LoadTable(string tableName);
        string GetTableName();
        Task<QueryResponse> QueryAsync(QueryRequest queryRequest);
        Task<ScanResponse> ScanAsync(ScanRequest scanRequest);
        Task<PutItemResponse> PutItemAsync(Document putItemRequest);
    }
}