using System.ComponentModel.DataAnnotations.Schema;

public abstract record class ModuleVariantBase : ModuleBase
{
    public double? ValueField { get; init; }
}

[Table("ModuleVariant", Schema = "iq4")]
public record class ModuleVariantA : ModuleVariantBase
{
    public double? ValueField { get; init; }
}

[Table("ModuleVariant", Schema = "iq2")]
public record class ModuleVariantB : ModuleVariantBase
{
    public double? RemoteLan { get; init; }
    public double? AlarmAdress { get; init; }
    public double? TextOnOff { get; init; }
    public string? SupplyFrequency { get; init; }
    public string? ReferenceVolts { get; init; }
}
