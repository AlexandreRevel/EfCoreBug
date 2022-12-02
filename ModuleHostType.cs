[Flags]
public enum ModuleHostType
{
    None = 0,
    VariantBB = 0b0000_0001 | VariantB,
    VariantB = 0b0000_1000,
    VariantA = 0b0001_0000,
    VariantC = 0b0010_0000,
}
