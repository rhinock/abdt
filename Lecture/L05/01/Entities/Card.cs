namespace _01.Entities
{
    public class Card : IEntity
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string PaymentSystem { get; set; }
        public Account Account { get; set; }
    }
}
