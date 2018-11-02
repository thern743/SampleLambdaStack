using Amazon.DynamoDBv2.Model;

namespace Lambda.Common.Utils
{
    public class PutItemRequestBuilder
    {
        private PutItemRequest _request;

        public PutItemRequestBuilder()
        {
            _request = new PutItemRequest();
        }
        
    }
}