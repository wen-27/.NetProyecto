using FluentValidation;

namespace Application.UseCase.ServiceTypes;

public sealed class CreateServiceTypeValidator : AbstractValidator<CreateServiceType>
{
    public CreateServiceTypeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del tipo de servicio es obligatorio.")
            .MaximumLength(80).WithMessage("El nombre del tipo de servicio no puede superar 80 caracteres.");
    }
}
