using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Syntax.Models;

public class Category
{
    [Key] public int Id { get; set; }

    public string? Name { get; set; }

    public string? Icon { get; set; }
    public string? Description { get; set; }
    public User User { get; set; }

    public string? Type { get; set; }
}