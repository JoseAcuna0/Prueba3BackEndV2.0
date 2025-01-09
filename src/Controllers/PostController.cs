using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApiPrueba3V2._00.src.Interfaces;
using ApiPrueba3V2._00.src.DTOs;
using System.Security.Claims;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace ApiPrueba3V2._00.src.Controllers
{
    [Route("api/posts")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly Cloudinary _cloudinary;

        public PostController(IPostRepository postRepository, Cloudinary cloudinary)
        {
            _postRepository = postRepository;
            _cloudinary = cloudinary;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postRepository.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDTO createPostDto)
        {
            if(createPostDto.url == null || createPostDto.url.Length == 0)
            {
                return BadRequest("Se requiere una imagen");
            }

            if(createPostDto.url.ContentType != "image/jpeg" && createPostDto.url.ContentType != "image/png")
            {
                return BadRequest("Solo se permiten imágenes JPEG o PNG");
            }

            if (createPostDto.url.Length > 5 * 1024 * 1024)
            {
                return BadRequest("La imagen es muy pesada (max 5MB)");
            }

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(createPostDto.url.FileName, createPostDto.url.OpenReadStream()),
                Folder = "posts"
            };

            var uploadResults = await _cloudinary.UploadAsync(uploadParams);

            if(uploadResults.Error != null)
            {
                return BadRequest(uploadResults.Error.Message);
            }

            if (createPostDto == null)
            {
                return BadRequest("Post is null.");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            string urlImage = uploadResults.SecureUrl.AbsoluteUri;

            var createdPost = await _postRepository.CreatePostAsync(createPostDto, userId, urlImage);
            return CreatedAtAction(nameof(GetAllPosts), new { id = createdPost.Id }, createdPost);
        }
    }
}