using System.ComponentModel.DataAnnotations;

namespace RequestValidation.Models
{
    public class Employee
    {
        [Range(10000, 90000)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        public int Department { get; set; }
    }
}