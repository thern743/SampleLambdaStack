namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class AddressDocument
    {
        public State State { get; set; }
        public PostalCode PostalCode { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public Line1 Line1 { get; set; }
        public Line2 Line2 { get; set; }
    }
}