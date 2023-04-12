
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Syntax.API.DI;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager config = builder.Configuration;

builder.Services.AddControllers();

#region DI
builder.Services.AddAuthServiceAsync(config);

builder.Services.AddInfStructDB(config);
builder.Services.AddSwaggerService();
#endregion


#region CORS
builder.Services.AddCors(policy =>
    policy.AddDefaultPolicy(p =>
        p.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));
#endregion

#region COOKIES
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "";
    options.AccessDeniedPath = "";
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddSession();


builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(TokenValidationFilter));
});

#endregion

var app = builder.Build();
// Configure the HTTP request pipeline.

#region SESSION

app.UseSession();
#endregion

#region SWAGGER
// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Syntax API V1");
});
#endregion

#region AUTH
app.UseAuthentication();
app.UseAuthorization();
#endregion

app.UseCors();

app.MapControllers();

app.Run();
