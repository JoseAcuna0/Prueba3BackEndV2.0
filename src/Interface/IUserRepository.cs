using System;
using System.Collections.Generic;
using System.Linq;
using ApiPrueba3V2._00.src.DTOs;
using System.Threading.Tasks;
using ApiPrueba3V2._00.src.Model;
using Microsoft.AspNetCore.Identity;

namespace ApiPrueba3V2._00.src.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterUserDTO registerUser);


    }
}