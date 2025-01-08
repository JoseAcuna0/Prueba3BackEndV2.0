using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiPrueba3V2._00.src.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }

        [MinLength(5, ErrorMessage = "El título debe tener al menos 5 caracteres.")]
        public required string title { get; set; }

        
        public required DateTime publishDate { get; set; } = DateTime.Now;

        [Url(ErrorMessage = "Debe ser una URL válida.")]
        public required string url { get; set; }

        public string UserId { get; set; } = null!;

    }
}