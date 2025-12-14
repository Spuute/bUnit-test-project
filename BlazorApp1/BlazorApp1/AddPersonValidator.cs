using BlazorApp1.Forms;
using FluentValidation;

namespace BlazorApp1;

public class AddPersonValidator : AbstractValidator<PersonForm>
{
    public AddPersonValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .Length(2, 150)
            .Matches("^[a-zA-Z0-9]*$")
            .WithMessage("First name must be between 2 and 150 characters long.");
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .Length(2, 150)
            .WithMessage("Last name must be between 2 and 150 characters long.");
        
        RuleFor(x => x.BirthYear)
            .NotEmpty()
            .NotEqual(0)
            .LessThanOrEqualTo(DateTime.Today.Year)
            .WithMessage("Birth year must be greater than or equal to zero.");
    }
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<PersonForm>.CreateWithOptions((PersonForm)model, x => x.IncludeProperties(propertyName)));
        
        if (result.IsValid)
        {
            return Array.Empty<string>();
        }
        
        return result.Errors.Select(e => e.ErrorMessage);
    };
}