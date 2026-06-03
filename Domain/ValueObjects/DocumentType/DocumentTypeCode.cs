using Domain.ValueObjects;

namespace Domain.ValueObjects.DocumentType;

// Value Object que encapsula y valida un valor especifico de DocumentTypeCode.
public readonly record struct DocumentTypeCode
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public DocumentTypeCode(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(DocumentTypeCode), 10);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
