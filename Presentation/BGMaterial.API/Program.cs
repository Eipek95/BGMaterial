using BGMaterial.API.ServiceRegistirations;
using BGMaterial.Domain.Entities;
using BGMaterial.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<MaterialContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), opt =>
    {
        opt.MigrationsAssembly(Assembly.GetAssembly(typeof(MaterialContext)).GetName().Name);
    });
});
builder.Services.AddApiService(builder.Configuration);
var app = builder.Build();



//seed region
#region seed
using (var scope = app.Services.CreateScope())
{
    var appDbContext = scope.ServiceProvider.GetRequiredService<MaterialContext>();
    appDbContext.Database.Migrate();
    if (!appDbContext.Materials.Any())
    {
        appDbContext.Materials.AddRange(new Material[]
        {
            new Material(){Code="0249.E9",Name="ÜST KAPAK CONTA",Model="BOXER-3/JUMPER-3",Engine="2.2 HDI",Year="06-",ListPrice=1144.78M,StockMrk=0,StockLzm=1,StockAnk=1,StockAdn=8,StockErz=1},
            new Material(){Code="0249.F4",Name="KÜLBÜTÖR KAPAK CONTASI",Model="1.6 EP6",Engine="1.6 EP6 ",Year="07-",ListPrice=837.47M,StockMrk=18,StockLzm=0,StockAnk=1,StockAdn=13,StockErz=11},
        });
        appDbContext.SaveChangesAsync().Wait();
    }

    if (!appDbContext.AppUsers.Any())
    {
        appDbContext.AppUsers.Add(new AppUser()
        {
            Username = "admin",
            Password = "Password12*"
        });

        appDbContext.SaveChangesAsync().Wait();
    }
}
#endregion


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BGMaterial API v1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
