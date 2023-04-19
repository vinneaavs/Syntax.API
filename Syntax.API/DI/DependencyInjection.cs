using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Syntax.API.Context;
using Syntax.API.Models;
using Syntax.Application.Interfaces.Services;
using Syntax.Auth.Configurations;
using Syntax.Auth.Data;
using Syntax.Auth.Services;
using System.Configuration;
using System.Text;

namespace Syntax.API.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfStructDB(this IServiceCollection services, IConfiguration configuration)
        {
            var provider =
                services
                .AddDbContext<ApplicationDbContext>(
                options =>
                options
                .UseSqlServer(configuration.GetConnectionString("DevConnections")))
            .BuildServiceProvider();

            var context = provider
                .GetRequiredService<ApplicationDbContext>();
            DbInitializer
                .Initialize(context);

            var contextIdnetity = provider.GetRequiredService<IdentityContext>();
            DbInitializer.Initialize(contextIdnetity);




            return services;
        }

        public static async Task<IServiceCollection> AddAuthServiceAsync(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<IdentityContext>(options => options
     .UseSqlServer(configuration.GetConnectionString("IdentityConnections"), b => b.MigrationsAssembly("Syntax.API")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();

            //TOKEN

            var jwtAppSettingsOptions = configuration.GetSection(nameof(JwtOptions));
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtOptions:SecurityKey").Value));

            //CRIANDO E POPULANDO O TIPO JWTOPTIONS (CRIADO PARA CONFIGURAÇÃO COM AS ENTIDADES NECESSARIAS)
            services.Configure<JwtOptions>(op =>
            {
                op.Issuer = jwtAppSettingsOptions[nameof(JwtOptions.Issuer)];
                op.Audience = jwtAppSettingsOptions[nameof(JwtOptions.Audience)];
                op.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
                op.Expiration = int.Parse(jwtAppSettingsOptions[nameof(JwtOptions.Expiration)] ?? "0");

            });

            services.Configure<IdentityOptions>(op =>
            {
                op.Password.RequireDigit = true;
                op.Password.RequireLowercase = true;
                op.Password.RequireNonAlphanumeric = true;
                op.Password.RequireUppercase = true;
                op.Password.RequiredLength = 6;
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = configuration.GetSection("JwtOptions:Issuer").Value,

                ValidateAudience = true,
                ValidAudience = configuration.GetSection("JwtOptions:Audience").Value,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero

            };

            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(op =>
            {
                op.RequireHttpsMetadata = false;
                op.SaveToken = true;
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtOptions:SecurityKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //services.AddAuthorization(op =>
            //{
            //    op.AddPolicy("AdminOnly", policy => policy.RequireRole("Administrator"));
            //});


            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            services.AddTransient<IJwtTokenValidator, JwtTokenValidator>();


            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(typeof(JwtAuthenticationFilter));
            //});

            return services;

        }


        public static async Task InitializeRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var rolesNames = new string[] { "Administrator", "User", "Premium" };

            foreach (var roleName in rolesNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Syntax API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Bearer space token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oath2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

            });

         
                services.AddScoped<SeedUsers>();
            services.AddScoped<SeedAsset>();
            services.AddScoped<SeedAssetClass>();

            var serviceProvider = services.BuildServiceProvider();

            #region CASO FAÇA UPDATEDATABSE OU MIGRATION COMENTAR
            InitializeRoles(serviceProvider).Wait();
            #endregion

            return services;
        }

    }
}
