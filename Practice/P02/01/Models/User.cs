using _01.Enums;

namespace _01.Models
{
    /// <summary>
    /// User model
    /// </summary>
    public class User
    {
        /// <summary>
        /// User id
        /// </summary>
        public long Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}
