using FluentValidation;

namespace Application.UseCase.PersonPhones;

// Caso de uso que modela una accion o consulta de negocio relacionada con AddPersonPhone.
public sealed class AddPersonPhoneValidator : AbstractValidator<AddPersonPhone>
{
    // El flujo debe permanecer enfocado en una sola operacion para facilitar pruebas y mantenimiento.
    public AddPersonPhoneValidator()
    {
        RuleFor(x => x.PersonId).GreaterThan(0).WithMessage("El identificador de la persona debe ser mayor que cero.");
        RuleFor(x => x.CountryId).GreaterThan(0).WithMessage("El identificador del país debe ser mayor que cero.");
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(30).WithMessage("El teléfono es obligatorio y no puede superar 30 caracteres.");
    }
}
