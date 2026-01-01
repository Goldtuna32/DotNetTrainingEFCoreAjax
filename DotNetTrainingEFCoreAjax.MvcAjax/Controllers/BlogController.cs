using DotNetTrainingEFCoreAjax.Database.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
