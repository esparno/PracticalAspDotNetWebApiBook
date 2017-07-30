using System.ComponentModel.DataAnnotations;
using Resources;
using System.Collections.Generic;
using System;

namespace RequestValidation.Models
{
    public class Employee : IValidatableObject
    {
        private const decimal PERCENTAGE = .75M;

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal AnnualIncome { get; set; }

        public decimal Contribution401K { get; set; }

        public IEnumerable<ValidationResult> Validate (ValidationContext context)
        {
            if (this.Id < 10000 || this.Id > 99999)
                yield return new ValidationResult("ID must be in the range 10000 - 99999");

            if (String.IsNullOrEmpty(this.LastName))
                yield return new ValidationResult("Last Name is mandatory");
            else if (this.LastName.Length > 20)
                yield return new ValidationResult(
                              "You can enter only 20 characters. No disrespect it is only a system constraint");

            if (this.Contribution401K > Decimal.Zero &&
                    this.Contribution401K > this.AnnualIncome * PERCENTAGE)
                yield return new ValidationResult(
                                          "You can contribute a maximum of 75% of your annual income to 401K");
        }
    }



    //public class Employee
    //{
    //    [Range(10000, 90000)]
    //    public int Id { get; set; }

    //    public string FirstName { get; set; }

    //    [Required]
    //    [MaxLength(20, ErrorMessageResourceName= "InvalidLastNameLength", ErrorMessageResourceType=typeof(Messages))]
    //    public string LastName { get; set; }

    //    [MemberRange(0,9)]
    //    public List<int> Department { get; set; }

    //    public decimal AnnualIncome { get; set; }

    //    [LimitChecker("AnnualIncome", 75)]
    //    public decimal Contribution401k { get; set; }
    //}
}