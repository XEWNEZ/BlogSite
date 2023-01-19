using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace  MyBlog.Controllers;

public class BlogController : Controller
{
    private readonly BlogContext _blogContext;

    public BlogController(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    [HttpGet]
    public IActionResult BlogList(){
        var blog = _blogContext.Blogs.ToList();
        foreach (var item in blog)
        {
            item.Author = _blogContext.Authors.Find(item.AuthorId);
            item.Category = _blogContext.Categories.Find(item.CategoryId);
        }
        return View(blog);
    }

    [HttpGet]
    public IActionResult BlogDetail(int id){
        List<Blog> blogs = _blogContext.Blogs.ToList();
        ViewBag.List = blogs;
        var blog = _blogContext.Blogs.Find(id);
        blog.Author = _blogContext.Authors.Find(blog.AuthorId);
        blog.Category = _blogContext.Categories.Find(blog.CategoryId);
        return View(blog);
    }

}