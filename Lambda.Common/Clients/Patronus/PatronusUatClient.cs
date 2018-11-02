using System;
using System.Linq;
using System.Threading.Tasks;
using Lambda.Common.Clients.Patronus.Interfaces;
using Lambda.Common.Extensions;
using Lambda.Common.Interfaces;
using Lambda.Common.Utils;

namespace Lambda.Common.Clients.Patronus
{
    public class PatronusUatClient : PatronusClient, IPatronusUatClient
    {
        public PatronusUatClient(IClientFactory<IDynamoClient> dynamoClientFactory, string tableName) : base(dynamoClientFactory, tableName)
        {
        }

        public async Task<PatronusGenerationResponse> GenerateRecord(string durable = null, string chd = null)
        {
            var chdUserTxsFromDurable = await GetPrimaryFromDurableId(durable);
            if (chdUserTxsFromDurable.Any()) return new PatronusGenerationResponse("Account with specified durable already exists.", 204);

            var chdUserTxsFromChd = await GetPrimaryFromChd(chd);
            if (chdUserTxsFromChd.Any()) return new PatronusGenerationResponse("Account with specified chd already exists.", 204);

            var newRecord = PatronusRecordGenerator.GenerateRecord(durable, chd);
            var doc = new DocumentBuilder()
                .AddProperty("ChdUserTx", newRecord.ChdUserTx.Value)
                .AddProperty("ChdAccountNumber", newRecord.ChdAccountNumber.Value)
                .AddProperty("CustomerDurableId", newRecord.CustomerDurableId.Value)
                .Build();

            var client = _dynamoClientFactory.GetClient(TableName);
            var response = await client.PutItemAsync(doc);
            var responseCode = (int)response.HttpStatusCode;

            return ResponseUtils.IsSuccessfulStatusCode(responseCode)
                ? new PatronusGenerationResponse("Record successfully generated.", MapDynamoResponse(responseCode), newRecord)
                : new PatronusGenerationResponse("Unable to generate record.", responseCode);
        }

        private int MapDynamoResponse(int statusCode) => statusCode == 200 ? 201 : statusCode;
    }
}