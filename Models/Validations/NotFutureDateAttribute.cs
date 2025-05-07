using System.ComponentModel.DataAnnotations;

namespace Homework1.Models.Validations
{
    public class NotFutureDateAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            return (DateTime)value <= DateTime.Today;
        }
    }

    public class PositiveIntegerAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            if (int.TryParse(value.ToString(), out int number))
            {
                return number > 0;
            }

            return false;
        }
    }
}
