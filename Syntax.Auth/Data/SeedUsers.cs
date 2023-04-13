using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Syntax.Auth.Data;

public class SeedUsers : IHostedService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public SeedUsers(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var admin1 = new ApplicationUser
        {
            Email = "vinicius.silva@atos.net",
            UserName = "vinicius.silva@atos.net",
            EmailConfirmed = true,
            Name = "Vinicius",
            LastName = "Silva",
            CreationDate = DateTime.Now,
            Role = "Administrator",
            IsEmailConfirmed = true
        };

        var admin2 = new ApplicationUser
        {
            Email = "allan.schramm@atos.net",
            UserName = "allan.schramm@atos.net",
            EmailConfirmed = true,
            Name = "Allan",
            LastName = "Schramm",
            CreationDate = DateTime.Now,
            Role = "Administrator",
            IsEmailConfirmed = true
        };

        if (await _userManager.FindByEmailAsync(admin1.Email) == null)
        {
            await _userManager.CreateAsync(admin1, "Syntax@123");
            await _userManager.AddToRoleAsync(admin1, "Administrator");

        }

        if (await _userManager.FindByEmailAsync(admin2.Email) == null)
        {
            await _userManager.CreateAsync(admin2, "Syntax@123");
            await _userManager.AddToRoleAsync(admin2, "Administrator");

        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
