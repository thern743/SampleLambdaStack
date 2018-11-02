using System;
using Bogus;
using Lambda.Common.Clients.Patronus.DynamoDto;

namespace Lambda.Common.Clients.Patronus
{
    public static class PatronusRecordGenerator
    {
        public static PatronusRecord GenerateRecord(string durable = null, string chd = null)
        {
            var patronusRecordFaker = new Faker<PatronusRecord>()
                    .RuleFor(o => o.ChdUserTx, f => new ChdUserTx { Value = f.Finance.Account(24).ToString() })
                    .RuleFor(o => o.CustomerDurableId, f => new CustomerDurableId { Value = Guid.NewGuid().ToString() })
                    .RuleFor(o => o.ChdAccountNumber, f => new ChdAccountNumber { Value = f.Finance.Account(16).ToString() })
                ;

            var newRecord = patronusRecordFaker.Generate();

            if (durable != null)
                newRecord.CustomerDurableId = new CustomerDurableId { Value = durable };

            if (chd != null)
                newRecord.ChdAccountNumber = new ChdAccountNumber { Value = chd };

            return newRecord;
        }
    }
}