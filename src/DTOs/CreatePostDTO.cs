using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiPrueba3V2._00.src.DTOs
{
    public class CreatePostDTO
    {
        [MinLength(5, ErrorMessage = "El título debe tener al menos 5 caracteres.")]
        public string title { get; set; } = null!;

        public IFormFile url { get; set; } = null!;

        
    }
}