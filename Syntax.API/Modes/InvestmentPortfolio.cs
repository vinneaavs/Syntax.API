﻿using System.ComponentModel.DataAnnotations;

namespace Syntax.Models
{
    public class InvestmentPortfolio
    {
        [Key]public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public User? User { get; set; }


    }
}
