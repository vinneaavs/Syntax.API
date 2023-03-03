
using Syntax.API.DI;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager config = builder.Configuration;

builder.Services.AddControllers();

#region DI
builder.Services.AddAuthService(config);

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
    options.LoginPath = "";
    options.LogoutPath = "";
    options.AccessDeniedPath = "";
});
#endregion


var app = builder.Build();
// Configure the HTTP request pipeline.


// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Syntax API V1");
});




#region AUTH
app.UseAuthentication();
app.UseAuthorization();
#endregion


app.MapControllers();

app.Run();
