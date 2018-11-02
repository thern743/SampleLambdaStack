namespace Lambda.Common.Clients.Patronus.DynamoDto
{
    public class Card
    {
        public CardSequenceCount CardSequenceCount { get; set; }
        public CardExpirationDate CardExpirationDate { get; set; }
        public NToken NToken { get; set; }
        public CardType CardType { get; set; }
        public ProductType ProductType { get; set; }
        public ClosedDate ClosedDate { get; set; }
        public CardStatus CardStatus { get; set; }
        public CardSubType CardSubType { get; set; }
    }
}