using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ModuleHost", Schema = "common")]
public class ModuleHost
{
    [Key]
    public int Id { get; init; }
    public Request Requests { get; set; }
    public Guid ReadingRequestGuid { get; set; }

    public virtual ModuleHostType ModuleHostType { get; set; }
    public string ModuleHostTypeName
    {
        set => ModuleHostType = (ModuleHostType)Enum.Parse(typeof(ModuleHostType), value, true);
        get => ModuleHostType.ToString();
    }
}
public class ModuleHostA : ModuleHost
{
    public override ModuleHostType ModuleHostType { get => ModuleHostType.VariantA; }
    public virtual ICollection<ModuleVariantA> Modules { get; init; } = new List<ModuleVariantA>();
}
public class ModuleHostB : ModuleHostA
{
    public override ModuleHostType ModuleHostType { get => ModuleHostType.VariantA; }
}
public class ModuleHostC : ModuleHost
{
    public override ModuleHostType ModuleHostType { get => ModuleHostType.VariantC; }
    public virtual ICollection<ModuleVariantB> ModuleVariantBs { get; init; } = new List<ModuleVariantB>();
}
public class UnknownModuleHost : ModuleHost
{

}