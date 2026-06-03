using Domain.ValueObjects;

namespace Domain.ValueObjects.Supplier;

// Value Object que encapsula y valida un valor especifico de SupplierName.
public readonly record struct SupplierName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public SupplierName(string value) => Value = ValueObjectValidation.Required(value, nameof(SupplierName), 120);
    public string Value { get; }
}

// Value Object que encapsula y valida un valor especifico de SupplierTaxId.
public readonly record struct SupplierTaxId
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public SupplierTaxId(string? value) => Value = ValueObjectValidation.Optional(value, nameof(SupplierTaxId), 30);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de SupplierPhone.
public readonly record struct SupplierPhone
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public SupplierPhone(string? value) => Value = ValueObjectValidation.Optional(value, nameof(SupplierPhone), 30);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de SupplierEmail.
public readonly record struct SupplierEmail
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public SupplierEmail(string? value) => Value = ValueObjectValidation.Optional(value, nameof(SupplierEmail), 120);
    public string? Value { get; }
}

// Value Object que encapsula y valida un valor especifico de SupplierStatus.
public readonly record struct SupplierStatus
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public SupplierStatus(bool value) => Value = value;
    public bool Value { get; }
}
