// Responsabilidad: Caso de uso de Application para ejecutar una operacion de negocio relacionada con AddPersonEmailValidator. Recibe comandos/consultas, aplica validaciones y coordina repositorios.
// Nota de mantenimiento: Debe mantenerse enfocado en una accion concreta para que sea facil de probar y mantener.
using FluentValidation;

namespace Application.UseCase.PersonEmails;

public sealed class AddPersonEmailValidator : AbstractValidator<AddPersonEmail>
{
    public AddPersonEmailValidator()
    {
        RuleFor(x => x.PersonId).GreaterThan(0).WithMessage("El identificador de la persona debe ser mayor que cero.");
        RuleFor(x => x.EmailDomainId).GreaterThan(0).WithMessage("El identificador del dominio debe ser mayor que cero.");
        RuleFor(x => x.EmailUser).NotEmpty().MaximumLength(100).WithMessage("El usuario del correo es obligatorio y no puede superar 100 caracteres.");
    }
}
