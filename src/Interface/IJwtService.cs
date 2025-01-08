using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPrueba3V2._00.src.Model;

namespace ApiPrueba3V2._00.src.Interface
{
    public interface IJwtService
    {
        string CreateToken(AppUser user);
    }
}