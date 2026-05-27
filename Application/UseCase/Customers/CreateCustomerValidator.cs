using FluentValidation;

namespace Application.UseCase.Customers;

public sealed class CreateCustomerValidator : AbstractValidator<CreateCustomer>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.PersonId)
            .GreaterThan(0).WithMessage("El identificador de la persona debe ser mayor que cero.");
    }
}
