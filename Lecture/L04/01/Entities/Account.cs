namespace _01.Entities
{
    public class Account
    {
        public long Id { get; set; }
        public string AccountNumber { get; set; }
        public long CardId { get; set; }
        public Card Card { get; set; }
    }
}
