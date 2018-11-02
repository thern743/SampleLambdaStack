using System.Collections.Generic;
using Lambda.Common.Clients.Patronus.Interfaces;

namespace Lambda.Common.Clients.Patronus
{
    public class PatronusGenerationResponse
    {
        public string Message { get; set; }
        public List<IPatronusRecord> Records { get; set; } = new List<IPatronusRecord>();
        public int StatusCode { get; set; }

        public PatronusGenerationResponse(string message, int statusCode, IPatronusRecord record)
        {
            InitBasicProperties(message, statusCode);
            Records.Add(record);
        }

        public PatronusGenerationResponse(string message, int statusCode, IEnumerable<IPatronusRecord> records)
        {
            InitBasicProperties(message, statusCode);
            Records.AddRange(records);
        }

        public PatronusGenerationResponse(string message, int statusCode)
        {
            InitBasicProperties(message, statusCode);
        }

        private void InitBasicProperties(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}