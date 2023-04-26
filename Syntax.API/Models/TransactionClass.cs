using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Syntax.API.Models;

public class TransactionClass
{
    [Key] public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public DateTime? CreationDate { get; set; } = DateTime.Now;

}