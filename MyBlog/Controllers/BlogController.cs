using Microsoft.AspNetCore.Mvc;

namespace  MyBlog.Controllers;

public class BlogController : Controller
{

    [HttpGet]
    public IActionResult BlogList(){
        return View();
    }

    [HttpGet]
    public IActionResult BlogDetail(){
        return View();
    }

}