using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPrueba3V2._00.src.DTOs;
using ApiPrueba3V2._00.src.Model;


namespace ApiPrueba3V2._00.src.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToUserDto(this AppUser userModel)
        {
            return new UserDTO
            {
                Id = userModel.Id,
                Email = userModel.Email ?? string.Empty,
                UserName = userModel.UserName ?? string.Empty
            };
        }
    }
}