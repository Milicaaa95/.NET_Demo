using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.DTOs
{
    public class CategoryDTO
    {
        [Required]
        [MaxLength(45)]
        public string Name { get; set; }
    }
}
