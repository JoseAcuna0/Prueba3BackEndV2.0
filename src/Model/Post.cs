using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApiPrueba3V2._00.src.Model
{
    public class Post
    {
        public int Id { get; set; }

        [MinLength(5, ErrorMessage = "El título debe tener al menos 5 caracteres.")]
        public string title { get; set; } = null!;

        
        public required DateTime publishDate { get; set; }

        [Url(ErrorMessage = "Debe ser una URL válida.")]
        public string url { get; set; } = null!;

        //relacion user y post
        public string UserId { get; set; } = null!;
    }
}