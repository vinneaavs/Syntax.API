﻿using System.ComponentModel.DataAnnotations;

namespace Syntax.API.Models
{
    public class AssetClass
    {
        [Key] public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;


    }
}
