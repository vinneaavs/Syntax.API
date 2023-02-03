using Microsoft.AspNetCore.Identity;
using Syntax.API.Context;
using Syntax.API.DI;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddInfStructDB(config);
builder.Services.AddAuthService(config);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();
builder.Services.Configure<IdentityOptions>(op =>
{
    op.Password.RequiredLength = 4;
    op.Password.RequireUppercase = false;
});

builder.Services.AddCors(policy =>
    policy.AddDefaultPolicy(p =>
        p.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "";
    options.LogoutPath = "";
    options.AccessDeniedPath = "";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
