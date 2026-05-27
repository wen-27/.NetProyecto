using Domain.ValueObjects;

namespace Domain.ValueObjects.CardType;

public readonly record struct CardTypeName
{
    public CardTypeName(string value) => Value = ValueObjectValidation.Required(value, nameof(CardTypeName), 50);
    public string Value { get; }
}
