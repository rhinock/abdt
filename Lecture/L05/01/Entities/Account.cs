using System.Collections.Generic;

namespace _01.Entities
{
    public class Account
    {
        public long Id { get; set; }
        public string AccountNumber { get; set; }
        public long CardId { get; set; }
        public Card Card { get; set; }
        public AccountData Data { get; set; }
    }

    public class AccountData
    {
        public Dictionary<string, string> Data { get; set; }
    }
}
