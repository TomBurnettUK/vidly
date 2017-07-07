using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class ValidNumberInStock : ValidationAttribute
    {
        const int MIN_VAL = 1;
        const int MAX_VAL = 20;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stock = (int?)value;

            if (stock == null)
            {
                return new ValidationResult($"The Number in Stock field is required");
            }
            if (stock.Value < MIN_VAL)
            {
                return new ValidationResult($"Number in stock must be greater than {MIN_VAL - 1}");
            }
            if (stock.Value > MAX_VAL)
            {
                return new ValidationResult($"Number in stock must be less than {MAX_VAL + 1}");
            }
            return ValidationResult.Success;
        }
    }
}