using DotNetTrainingEFCoreAjax.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingEFCoreAjax.MvcAjax.Controllers
{
    public class BlogController : Controller
    {

        private readonly AppDbContext _db;

        public BlogController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogs = await _db.BlogTables
                            .OrderByDescending(x => x.BlogId)
                            .ToListAsync();
            return Json(blogs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogTable blog)
        {
            await _db.BlogTables.AddAsync(blog);
            var result = await _db.SaveChangesAsync();
            var response = new
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Saving Successful" : "Failed to save"
            };
            return Json(response);
        }

        
        [HttpGet]
        public async Task<IActionResult> EditBlog(int id)
        {
            var blog = await _db.BlogTables.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
            if (blog is null)
            {
                return Json(new { IsSuccess = false, Message = "Blog not found." });
            }
            return Json(blog);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.BlogId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogTable blog)
        {
            var item = await _db.BlogTables.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return Json(new { IsSuccess = false, Message = "Blog not found" });
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            var result = await _db.SaveChangesAsync();
            var response = new
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Update Successful" : "Failed to update"
            };
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.BlogTables.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return Json(new { IsSuccess = false, Message = "Blog not found" });
            }
            _db.BlogTables.Remove(item);
            var result = await _db.SaveChangesAsync();
            var response = new
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Delete Successful" : "Failed to delete"
            };
            return Json(response);
        }

        
    }
}
