using Domain.ValueObjects;

namespace Domain.ValueObjects.CardType;

// Value Object que encapsula y valida un valor especifico de CardTypeName.
public readonly record struct CardTypeName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public CardTypeName(string value) => Value = ValueObjectValidation.Required(value, nameof(CardTypeName), 50);
    public string Value { get; }
}
