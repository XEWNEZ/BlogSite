using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlogContext _context;

    public HomeController(ILogger<HomeController> logger, BlogContext context)
    {
        _logger = logger;
        _context = context;
    }
#region Sayfalar
        public IActionResult Index()
        {
            List<Blog> blog = _context.Blogs.ToList();
            foreach (var item in blog)
            {
                item.Category = _context.Categories.Find(item.CategoryId);
                item.Author = _context.Authors.Find(item.AuthorId);
            }
            ViewBag.categories =_context.Categories.ToList();
            ViewBag.authors =_context.Authors.ToList();
            ViewBag.blogs = _context.Blogs.ToList();
            return View();
        }
        public IActionResult AboutUs(){
            return View();
        }
         public IActionResult Contact(){
            return View();
         }
#endregion



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
