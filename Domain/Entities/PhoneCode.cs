using System;
using Domain.Common;

namespace Domain.Entities;

public class PhoneCode : BaseEntity
{
    public int PersonId { get; set; }
    public Int PhoneCodeId { get; set; }
    public string Number { get; set; } = string.Empty;

    public Person Person { get; set; } = null!;
    public PhoneCode PhoneCode { get; set; } = null!;
}
