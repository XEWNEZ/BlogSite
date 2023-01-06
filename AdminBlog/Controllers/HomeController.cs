﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AdminBlog.Models;
using AdminBlog.Filter;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminBlog.Controllers;
//[UserFilter]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BlogContext _blogContext;
    public HomeController(ILogger<HomeController> logger, BlogContext blogContext)
    {
        _logger = logger;
        _blogContext = blogContext;
    }

    public IActionResult Index()
    {
        return View();
    }
#region Blog Actions
    public IActionResult BlogList()
    {
        ViewBag.Categories = _blogContext.Categories.Select(w=> new SelectListItem{
            Text = w.Name,
            Value = w.Id.ToString()
        }).ToList();
        return View();
    }
    public async Task<IActionResult> BlogCreate(Blog modal)
    {
        if (modal != null){
            var file = Request.Form.Files.First();
            //C:\Users\eren_\OneDrive\Masaüstü\Bloger\BlogSite\MyBlog\wwwroot\images\post-images
            string savePath = Path.Combine("C:","User","eren_","OneDrive","Masaüstü","Bloger","BlogSite","MyBlog","wwwroot","images","post-images");
            var fileName = $"{DateTime.Now:MMddHHmmss}.{file.FileName.Split(".").Last()}";
            var fileUrl = Path.Combine(savePath, fileName);
            using (var fileStream = new FileStream(fileUrl,FileMode.Create)){
                await file.CopyToAsync(fileStream);
            }
            modal.ImagePath = fileName;
            modal.AuthorId = (int)HttpContext.Session.GetInt32("id");
            await _blogContext.AddAsync(modal);
            await _blogContext.SaveChangesAsync();
            return Json(true);

        }
        return Json(false);
    }
#endregion
#region Category Actions
    [HttpPost]
    public async Task<IActionResult> CategoryCreate(Category category)
    {
        if(category.Id == 0){
            await _blogContext.AddAsync(category);
        }
        else{
            _blogContext.Update(category);
        }
        await _blogContext.SaveChangesAsync();
        return RedirectToAction(nameof(CategoryList));
    }
    public IActionResult CategoryList()
    {
        List<Category> categories = _blogContext.Categories.ToList();
        return View(categories);
    } 
    public async Task<IActionResult> CategoryDelete(int? id){
        Category category = await _blogContext.Categories.FindAsync(id);
        _blogContext.Remove(category);
        await _blogContext.SaveChangesAsync();
        return RedirectToAction(nameof(CategoryList));
    } 
    public async Task<IActionResult> CategoryUpdate(int id){
        Category category = await _blogContext.Categories.FindAsync(id);
        return Json(category);
    } 
#endregion
#region Author Actions
    public IActionResult AuthorCreate(int? id){
        if(id == 0)
            return View();
        else{
            ViewBag.AuthorId = id;
            return View();
        }
    }
    public async Task<IActionResult> AuthorAdd(Author author){
        if(author.Id == 0){
            _blogContext.AddAsync(author);
        }
        else{
            _blogContext.Update(author);
        }
        await _blogContext.SaveChangesAsync();
        return RedirectToAction(nameof(AuthorList));
    }
    public IActionResult AuthorList(Author author){
        List<Author> authors = _blogContext.Authors.ToList();
        return View(authors);
    }
    public async Task<IActionResult> AuthorDelete(int? id){
        Author author = await _blogContext.Authors.FindAsync(id);
        _blogContext.Remove(author);
        await _blogContext.SaveChangesAsync();
        return RedirectToAction(nameof(AuthorList));
    }
    public async Task<IActionResult> AuthorUpdate(int id){
        Author author = await _blogContext.Authors.FindAsync(id);
        return Json(author);
    }
#endregion
#region Diğer Actions
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
#endregion
}
