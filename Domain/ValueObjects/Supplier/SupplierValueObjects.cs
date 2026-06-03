// Responsabilidad: Value Object de dominio para validar y encapsular un valor especifico de SupplierValueObjects, evitando datos invalidos en el resto del sistema.
// Nota de mantenimiento: Debe mantenerse pequeno, inmutable cuando aplique y enfocado en validar una sola idea del dominio.
using Domain.ValueObjects;

namespace Domain.ValueObjects.Supplier;

public readonly record struct SupplierName
{
    public SupplierName(string value) => Value = ValueObjectValidation.Required(value, nameof(SupplierName), 120);
    public string Value { get; }
}

public readonly record struct SupplierTaxId
{
    public SupplierTaxId(string? value) => Value = ValueObjectValidation.Optional(value, nameof(SupplierTaxId), 30);
    public string? Value { get; }
}

public readonly record struct SupplierPhone
{
    public SupplierPhone(string? value) => Value = ValueObjectValidation.Optional(value, nameof(SupplierPhone), 30);
    public string? Value { get; }
}

public readonly record struct SupplierEmail
{
    public SupplierEmail(string? value) => Value = ValueObjectValidation.Optional(value, nameof(SupplierEmail), 120);
    public string? Value { get; }
}

public readonly record struct SupplierStatus
{
    public SupplierStatus(bool value) => Value = value;
    public bool Value { get; }
}
