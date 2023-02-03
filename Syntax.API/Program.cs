using Syntax.API.DI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

ConfigurationManager config = builder.Configuration;
// Add services to the container.
builder.Services.AddInfStructDB(config);

builder.Services.AddControllers();
builder.Services.AddInfraStructureDb(config);

builder.Services.AddCors(policy =>
    policy.AddDefaultPolicy(p =>
        p.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
