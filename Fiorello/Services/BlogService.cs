using Microsoft.EntityFrameworkCore;
using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services.Interfeices;
using Fiorello.ViewModels;
using System;

namespace Fiorello.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;

        public BlogService(AppDbContext context)
        {
            _context = context;
                
        }
        public async Task<List<BlogVM>> GetAllBlogs(int? take=null)
        {
            List<BlogVM> blogVMs=new List<BlogVM>();

            if(take.HasValue)
            {
                blogVMs = await _context.Blogs
                .Select(b => new BlogVM
                {
                    Title = b.Title,
                    Description = b.Description,
                    CreatedAt = b.CreatedAt,
                    ImageUrl = b.ImageUrl,
                    Id = b.Id
                })
                .Take((int)take)
                .ToListAsync();
            }
            else
            {
                blogVMs = await _context.Blogs
                .Select(b => new BlogVM
                {
                    Title = b.Title,
                    Description = b.Description,
                    CreatedAt = b.CreatedAt,
                    ImageUrl = b.ImageUrl,
                    Id = b.Id
                })
                .ToListAsync();
            }
            return blogVMs;


        }

        public async Task<BlogVM> GetBlogByIdAsync(int id)
        {
           
            List<BlogVM> blog = await _context.Blogs.Select(n=> new BlogVM
            {
                Title = n.Title,
                Description = n.Description,
                CreatedAt = n.CreatedAt,
                ImageUrl = n.ImageUrl,
                Id = n.Id

            } ).ToListAsync();

            var blogId = blog.FirstOrDefault(b => b.Id == id);  

            return (blogId);
        }

    }
}
