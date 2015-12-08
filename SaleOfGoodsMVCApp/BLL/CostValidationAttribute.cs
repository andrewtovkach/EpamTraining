using System.ComponentModel.DataAnnotations;

namespace BLL
{
    public class CostValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int temp = (int) value;
            return temp > 0;
        }
    }
}