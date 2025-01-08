using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ApiPrueba3V2._00.src.DTOs
{
    public class RegisterUserDTO
    {

        [EmailAddress]
        public string email { get; set; } = null!;

        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[0-9]).*$", ErrorMessage = "La contraseña debe contener al menos un número.")]
        public string password { get; set; } = null!;
    }
}