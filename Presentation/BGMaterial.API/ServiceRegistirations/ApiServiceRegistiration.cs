using BGMaterial.API.Filters;
using BGMaterial.Application.Dtos;
using BGMaterial.Application.Interfaces;
using BGMaterial.Application.Services;
using BGMaterial.Application.Tools;
using BGMaterial.Application.Validators.MaterialValidators;
using BGMaterial.Persistence.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace BGMaterial.API.ServiceRegistirations
{
    public static class ApiServiceRegistiration
    {
        public static void AddApiService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CreateMaterialValidator>());
            services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UpdateMaterialValidator>());

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAppUserRepository), typeof(AppUserRepository));
            services.AddScoped(typeof(IMaterialService), typeof(MaterialService));
            services.AddScoped(typeof(ValidateFilterAttribute<>));
            services.AddScoped<IValidator<CreateMaterialDto>, CreateMaterialValidator>();
            services.AddScoped<IValidator<UpdateMaterialDto>, UpdateMaterialValidator>();
            services.AddApplicationService(configuration);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BGMaterial API", Description = "Emre İPEK", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter your token in the text input below. Bearer prefix will be added automatically.(Aşağıdaki metin girişine jetonunuzu girin. Taşıyıcı öneki yani Bearer otomatik olarak eklenecektir eklemenize gerek yoktur.)",
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
        }
    }
}
