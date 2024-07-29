using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public static class ProductValidator
{
    public static List<string> ValidateProductViewModel(ProductViewModel viewModel)
    {
        var errors = new List<string>();
        var context = new ValidationContext(viewModel, serviceProvider: null, items: null);
        var results = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(viewModel, context, results, true);
        if (!isValid)
        {
            errors.AddRange(results.Select(result => result.ErrorMessage));
        }

        return errors;
    }
}