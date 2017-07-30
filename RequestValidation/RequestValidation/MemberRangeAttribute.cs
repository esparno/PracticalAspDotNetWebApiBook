using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RequestValidation
{
    public class MemberRangeAttribute : RangeAttribute
    {
        public MemberRangeAttribute(int min, int max) : base(min, max) { }

        public override bool IsValid(object value)
        {
 	         if (value is ICollection)
             {
                 var items = (ICollection)value;
                 return items.Cast<int>().All(i => IsValid(i));
             }
             else
             {
                 return base.IsValid(value);
             }
        }
    }
}