using Microsoft.EntityFrameworkCore;

var ctx = GetContext();
var request = new Request
{
    CompanyName = "MyCompany",
    Mail = "mymail@mail.com",
    RequestName = "Request Name",
    SiteName = "My Site",
    Hosts = new List<ModuleHost>() { }
};
ctx.Add(request);
ctx.SaveChanges();

var address = new ModuleVariantA // <= The child
{
    ModuleIdentifier = "ModuleIdentifier",
    ModuleHost = new ModuleHostA // <= The Parent 
    {
        ReadingRequestGuid = request.RequestGuid,
        Modules = new List<ModuleVariantA>() { },
    }
};

ctx.Attach(address);
ctx.SaveChanges();

Console.ReadLine();

static MyContext GetContext()
{
    var factory = new MyContextFactory();

    var ctx = factory.CreateDbContext(Array.Empty<string>());

    ctx.Database.EnsureCreated();

    return ctx;
}
