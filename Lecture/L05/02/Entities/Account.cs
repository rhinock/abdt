using System.Collections.Generic;
using _02.Repositories;

namespace _02.Entities
{
    public class Account : IEntity
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public long CardId { get; set; }
        public Card Card { get; set; }

        public string Currency { get; set; }

        public AccountData Fields { get; set; }

    }

    public class AccountData
    {
        public Dictionary<string, string> Fields { get; set; }
    }
}