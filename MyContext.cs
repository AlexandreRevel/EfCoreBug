using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class MyContextFactory : IDesignTimeDbContextFactory<MyContext>
{
    public MyContext CreateDbContext(string[] args)
    {
        var connectionString = "User Id=XX;Password=XX;TrustServerCertificate=True;data source=localhost;initial catalog=EFCore.Test;MultipleActiveResultSets=True;App=EntityFramework";
        var options = new DbContextOptionsBuilder<MyContext>()
                   .UseSqlServer(connectionString, x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                   .EnableSensitiveDataLogging()
                   .Options;

        return new MyContext(options);
    }
}

public class MyContext : DbContext
{
    public DbSet<Request> Requests { get; set; } = default!;
    public DbSet<ModuleHost> ModuleHosts { get; set; } = default!;
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<TimeSpan>()
            .HaveConversion<string>();

        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("common");

        modelBuilder.Entity<Request>(x =>
        {
            x.HasKey(x => x.RequestGuid).IsClustered(false);
            x.Property(x => x.Id).UseIdentityColumn();
            x.HasIndex(x => x.Id).IsClustered(true);


            x.HasMany(x => x.Hosts)
            .WithOne(x => x.Requests)
            .HasForeignKey(x => x.ReadingRequestGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        });
        modelBuilder.Entity<ModuleHost>(x =>
        {
            x.Ignore(x => x.ModuleHostType);
            x.UseTphMappingStrategy();
            x.HasDiscriminator(x => x.ModuleHostTypeName)
                .HasValue<ModuleHostC>(ModuleHostType.VariantB.ToString())
                .HasValue<ModuleHostA>(ModuleHostType.VariantA.ToString())
                .HasValue<ModuleHostB>(ModuleHostType.VariantC.ToString())
                .HasValue<UnknownModuleHost>(ModuleHostType.None.ToString())
                .IsComplete(false);

            x.HasKey(x => x.Id);
            x.Property(x => x.Id).UseIdentityColumn();
        });

        modelBuilder.Entity<ModuleBase>().UseTpcMappingStrategy();
        modelBuilder.Entity<ModuleVariantBase>().UseTpcMappingStrategy();
        modelBuilder.Entity<ModuleVariantA>()
            .HasOne(x => x.ModuleHost as ModuleHostA)
            .WithMany(x => x.Modules)
            .IsRequired()
            .HasForeignKey(x => x.ModuleHostId);

        modelBuilder.Entity<ModuleVariantB>()
            .HasOne(x => x.ModuleHost as ModuleHostC)
            .WithMany(x => x.ModuleVariantBs)
            .IsRequired()
            .HasForeignKey(x => x.ModuleHostId);


        base.OnModelCreating(modelBuilder);
    }
}
