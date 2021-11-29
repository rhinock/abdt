using System.ComponentModel.DataAnnotations;

namespace _01.Models
{
    public class AddUserViewModel
    {
        public long? Id { get; set; }

        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(18, 100)]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
