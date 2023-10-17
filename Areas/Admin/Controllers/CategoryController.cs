using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnMoreApp.Data;
using LearnMoreApp.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LearnMoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Categories = await _context.Categories.ToListAsync();
            return View(Categories);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            Category category = await _context.Categories.FindAsync(id);

            if (category is null)
                return NotFound();
            return View(category);
        }
            public  IActionResult Create()
        {
            return  View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                Category newCategory = new Category()
                {
                    Title = category.Title,
                    Description = category.Description,
                    ThumbnailImagePath = category.ThumbnailImagePath
                };
               await _context.Categories.AddAsync(newCategory);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
                TempData["Success1"] = $"New Category Added Successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {

            if (id is null)
                return NotFound();
            Category category = await _context.Categories.FindAsync(id);
            if (category is null)
                return NotFound();
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromRoute] int? id, Category model)
        {
            if (id != model.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    Category category =await _context.Categories.FindAsync(id);
                    category.Title = model.Title;
                    category.Description = model.Description;
                    category.ThumbnailImagePath = model.ThumbnailImagePath;
                   _context.Categories.Update(category);
                    try
                    {
                       await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                    TempData["Success1"] = $"{model.Title} Data Updated Successfully";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(model);
                }
            }
            return View(model);
        }
        public async Task<IActionResult> CheckToDelete(int? id)
        {
            if (id is null)
                return NotFound();
            Category category =await _context.Categories.FindAsync(id);
            if (category is null)
                return NotFound();
            return View(category);
        }

        public async Task<IActionResult> Delete([FromRoute] int? id, Category category)
        {
            TempData["Success1"] = $"Data Of Category With Id {id} deleted Successfully";
            if (category is null)
                return NotFound();
             _context.Categories.Remove(category);
            try
            {
               await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
