using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPrueba3V2._00.src.Interfaces;
using ApiPrueba3V2._00.src.DTOs;
using Microsoft.AspNetCore.Identity;
using ApiPrueba3V2._00.src.Model;
using ApiPrueba3V2._00.src.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ApiPrueba3V2._00.src.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDBContext _context;


        public UserRepository(ApplicationDBContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

         public async Task<IdentityResult> RegisterAsync(RegisterUserDTO registerUser)
        {
            var user = new AppUser
            {
                UserName = registerUser.email,
                Email = registerUser.email
            };

            var result = await _userManager.CreateAsync(user, registerUser.password);

            return result;
        }

        
    }
}