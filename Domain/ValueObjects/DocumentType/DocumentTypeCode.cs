// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de DocumentTypeCode, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.DocumentType;

public readonly record struct DocumentTypeCode
{
    public DocumentTypeCode(string value)
    {
        Value = ValueObjectValidation.Required(value, nameof(DocumentTypeCode), 10);
    }
    public string Value { get; }
    public override string ToString() => Value;
}
