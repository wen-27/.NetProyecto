using Domain.ValueObjects;

namespace Domain.ValueObjects.PartBrand;

// Value Object que encapsula y valida un valor especifico de PartBrandName.
public readonly record struct PartBrandName
{
    // La validacion se concentra aqui para impedir que valores invalidos circulen por el dominio.
    public PartBrandName(string value) => Value = ValueObjectValidation.Required(value, nameof(PartBrandName), 80);
    public string Value { get; }
}
