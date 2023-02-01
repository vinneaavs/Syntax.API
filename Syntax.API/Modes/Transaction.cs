﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Syntax.Models;


public class Transaction
{
    [Key] public int Id { get; set; }
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public decimal Value { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string? Type { get; set; }

    public User User { get; set; }
    public Category Category { get; set; }

}