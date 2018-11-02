using Lambda.Common.Clients.Patronus.DynamoDto;

namespace Lambda.Common.Clients.Patronus.Interfaces
{
    public interface IPatronusRecord
    {
        AccountHolderType AccountHolderType { get; set; }
        Address Address { get; set; }
        CustomerDurableId CustomerDurableId { get; set; }
        EmployeeNumber EmployeeNumber { get; set; }
        ChdAccountNumber ChdAccountNumber { get; set; }
        ChdUserTx ChdUserTx { get; set; }
        Cards Cards { get; set; }
        Name Name { get; set; }
    }
}