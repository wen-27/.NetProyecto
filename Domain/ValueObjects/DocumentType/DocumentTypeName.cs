using Domain.ValueObjects;

namespace Domain.ValueObjects.DocumentType;

// Value Object que encapsula y valida un valor especifico de DocumentTypeName.
public readonly record struct DocumentTypeName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public DocumentTypeName(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(DocumentTypeName), 80);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
