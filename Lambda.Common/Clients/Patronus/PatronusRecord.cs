using Lambda.Common.Clients.Patronus.DynamoDto;
using Lambda.Common.Clients.Patronus.Interfaces;

namespace Lambda.Common.Clients.Patronus
{
    public class PatronusRecord : IPatronusRecord
    {
        public AccountHolderType AccountHolderType { get; set; }
        public Address Address { get; set; }
        public CustomerDurableId CustomerDurableId { get; set; }
        public EmployeeNumber EmployeeNumber { get; set; }
        public ChdAccountNumber ChdAccountNumber { get; set; }
        public ChdUserTx ChdUserTx { get; set; }
        public Cards Cards { get; set; }
        public Name Name { get; set; }
    }
}