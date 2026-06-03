// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de CardTypeValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.CardType;

public readonly record struct CardTypeName
{
    public CardTypeName(string value) => Value = ValueObjectValidation.Required(value, nameof(CardTypeName), 50);
    public string Value { get; }
}
