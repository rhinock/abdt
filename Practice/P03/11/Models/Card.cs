using System;

namespace _11.Models
{
    public class Card
    {
        public Guid Id { get; set; }

        public string Cvc { get; set; }

        public string Pan { get; set; }

        public Expire Expire { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        public Guid UserId { get; set; }
    }
}
