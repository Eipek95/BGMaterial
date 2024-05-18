
using BGMaterial.API.Filters;
using BGMaterial.Application.Dtos;
using BGMaterial.Application.Interfaces;
using BGMaterial.Application.Services;
using BGMaterial.Application.Tools;
using BGMaterial.Application.Validators.MaterialValidators;
using BGMaterial.Domain.Entities;
using BGMaterial.Persistence.Context;
using BGMaterial.Persistence.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CreateMaterialValidator>());
builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UpdateMaterialValidator>());


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidAudience = JwtTokenDefaults.ValidAudience,
        ValidIssuer = JwtTokenDefaults.ValidIssuer,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<MaterialContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IAppUserRepository), typeof(AppUserRepository));
builder.Services.AddScoped(typeof(IMaterialService), typeof(MaterialService));
builder.Services.AddScoped(typeof(ValidateFilterAttribute<>));
builder.Services.AddScoped<IValidator<CreateMaterialDto>, CreateMaterialValidator>();
builder.Services.AddScoped<IValidator<UpdateMaterialDto>, UpdateMaterialValidator>();
builder.Services.AddApplicationService(builder.Configuration);


//builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BGMaterial API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter your token in the text input below. Bearer prefix will be added automatically.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
      {
          new OpenApiSecurityScheme
          {
              Reference = new OpenApiReference
              {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
              }
          },
          new string[]{}
      }
  });
});
var app = builder.Build();
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

// Configure the HTTP request pipeline.
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
