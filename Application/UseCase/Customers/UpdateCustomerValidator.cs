using FluentValidation;

namespace Application.UseCase.Customers;

public sealed class UpdateCustomerValidator : AbstractValidator<UpdateCustomer>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El identificador del cliente debe ser mayor que cero.");
    }
}
