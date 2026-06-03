// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con UpdatePersonPhoneValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.PersonPhones;

public sealed class UpdatePersonPhoneValidator : AbstractValidator<UpdatePersonPhone>
{
    public UpdatePersonPhoneValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("El identificador del teléfono debe ser mayor que cero.");
        RuleFor(x => x.CountryId).GreaterThan(0).WithMessage("El identificador del país debe ser mayor que cero.");
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(30).WithMessage("El teléfono es obligatorio y no puede superar 30 caracteres.");
    }
}
