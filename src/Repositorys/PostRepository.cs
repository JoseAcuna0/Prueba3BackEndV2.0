using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPrueba3V2._00.src.Interfaces;
using ApiPrueba3V2._00.src.DTOs;
using Microsoft.AspNetCore.Identity;
using ApiPrueba3V2._00.src.Data;
using Microsoft.EntityFrameworkCore;
using ApiPrueba3V2._00.src.Model;


namespace ApiPrueba3V2._00.src.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDBContext _context;

        public PostRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<PostDTO> CreatePostAsync(CreatePostDTO postDto, string userId)
        {
            var post = new Post
            {
                title = postDto.title,
                publishDate = DateTime.Now,
                url = postDto.url,
                UserId = userId
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return new PostDTO
            {
                Id = post.Id,
                title = post.title,
                url = post.url,
                publishDate = post.publishDate,
                UserId = post.UserId
            };
        }

        public async Task<IEnumerable<PostDTO>> GetAllPostsAsync()
        {
            return await _context.Posts
                .Select(post => new PostDTO
                {
                    Id = post.Id,
                    title = post.title,
                    url = post.url,
                    publishDate = post.publishDate
                }).ToListAsync();
        }

    }
}