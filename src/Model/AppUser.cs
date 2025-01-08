using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ApiPrueba3V2._00.src.Model
{
    public class AppUser : IdentityUser
    {

        public ICollection<Post> Posts { get; set; } = new List<Post>();
        
    }
}