using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPrueba3V2._00.src.DTOs;

namespace ApiPrueba3V2._00.src.Interfaces
{
    public interface IPostRepository
    {
        Task<PostDTO> CreatePostAsync(CreatePostDTO createPostDto, string userId);
        Task<IEnumerable<PostDTO>> GetAllPostsAsync();
    }
}