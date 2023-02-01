using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Syntax.Models;


public class Transaction
{
    [Key] public int Id { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public decimal Value { get; set; }
    [Column(TypeName = "nvarchar(100)")] public string? Note { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string? Description { get; set; }   
    public string? Type { get; set; }
    public User User { get; set; }
}