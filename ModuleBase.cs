using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract record class ModuleBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public ModuleHost ModuleHost { get; set; }
    public int ModuleHostId { get; set; }
    public string? ModuleIdentifier { get; init; }
}
