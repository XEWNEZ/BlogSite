using AdminBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminBlog.Controllers
{
    public class AccountController : Controller{
        private readonly ILogger<HomeController> _logger;
        private readonly BlogContext _blogContext;
        public AccountController(ILogger<HomeController> logger, BlogContext blogContext)
        {
            _logger = logger;
            _blogContext = blogContext;
        }
        #region Login Action
            [HttpGet]
            public IActionResult Login(){
                if(HttpContext.Session.GetInt32("id").HasValue){
                    return Redirect("/Home/Index");
                }
                return View();
            }
            [HttpPost]
            public IActionResult Login(string Email, string Password){
                var author = _blogContext.Authors.FirstOrDefault(w=>w.Email == Email && w.Password == Password);
                if(author != null){
                    HttpContext.Session.SetInt32("id", author.Id);
                    HttpContext.Session.SetString("username", author.Name + " " + author.Surname);
                    return Redirect("/Home/Index");
                }  
                    return RedirectToAction(nameof(Login));
            }
            public IActionResult Logout(){
                HttpContext.Session.Clear();
                return RedirectToAction(nameof(Login));
            }
        #endregion

    }
}