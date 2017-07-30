using System.ComponentModel.DataAnnotations;
using Resources;
using System.Collections.Generic;

namespace RequestValidation.Models
{
    public class Employee
    {
        [Range(10000, 90000)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessageResourceName= "InvalidLastNameLength", ErrorMessageResourceType=typeof(Messages))]
        public string LastName { get; set; }

        [MemberRange(0,9)]
        public List<int> Department { get; set; }
    }
}