using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnMoreApp.Data;
using LearnMoreApp.Entities;
using LearnMoreApp.Extentions;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace LearnMoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CategoryItem
        public async Task<IActionResult> Index(int categoryId)
        {
            // To Get The Items Of A Specific Category By Its Id
            // And Check To Display An Add Or Update Content ReLated To The Current Item
            List<CategoryItem> CategoryItems = await (from CatItem in _context.CategoryItems
                                                      join ContentItem in _context.Contents
                                                      on CatItem.Id equals ContentItem.CategoryItem.Id
                                                      into GroupJoin
                                                      from SubContent in GroupJoin.DefaultIfEmpty()
                                                      where CatItem.CategoryId == categoryId
                                                      select new CategoryItem()
                                                      {
                                                          Id = CatItem.Id,
                                                          Title = CatItem.Title,
                                                          Description = CatItem.Description,
                                                          DateTimeItemReleased = CatItem.DateTimeItemReleased,
                                                          MediaTypeId = CatItem.MediaTypeId,
                                                          CategoryId = categoryId,
                                                          ContentId = (SubContent!=null) ? SubContent.Id : 0
                                                      }).ToListAsync();
            ViewBag.CategoryId = categoryId;

            return View(CategoryItems);
        }

        // GET: Admin/CategoryItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryItem = await _context.CategoryItems.FirstOrDefaultAsync(m => m.Id == id);

            if (categoryItem == null)
            {
                return NotFound();
            }

            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Create
        public async Task<IActionResult> Create(int categoryId)
        {
            List<MediaType> mediaTypes = await _context.MediaTypes.ToListAsync();

            CategoryItem categoryItem = new CategoryItem
            {
                CategoryId = categoryId,
                MediaTypes = mediaTypes.ConvertToSelectList(0)
            };

            return View(categoryItem);
        }

        // POST: Admin/CategoryItem/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create
        ([Bind("Id,Title,Description,CategoryId,MediaTypeId,DateTimeItemReleased")] CategoryItem categoryItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryItem);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
                return RedirectToAction(nameof(Index), new { categoryId = categoryItem.CategoryId});
            }
            //----------------------------------------------
            // In Case ModelState.IsValid == false
            // We Must Return The View With The Related Data Of The MediaTypes

            List<MediaType> mediaTypes = await _context.MediaTypes.ToListAsync();

            categoryItem.MediaTypes = mediaTypes.ConvertToSelectList(categoryItem.MediaTypeId);
            //----------------------------------------------

            return View(categoryItem);
        }

        // GET: Admin/CategoryItem/Update/5
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<MediaType> mediaTypes = await _context.MediaTypes.ToListAsync();

            var categoryItem = await _context.CategoryItems.FindAsync(id);

            if (categoryItem == null)
            {
                return NotFound();
            }
            categoryItem.MediaTypes = mediaTypes.ConvertToSelectList(categoryItem.MediaTypeId);
            return View(categoryItem);
        }

        // POST: Admin/CategoryItem/Update/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update
        (int id, [Bind("Id,Title,Description,CategoryId,MediaTypeId,DateTimeItemReleased")] CategoryItem categoryItem)
        {
            if (id != categoryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryItemExists(categoryItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { categoryId = categoryItem.CategoryId });
            }
            return View(categoryItem);
        }
        public async Task<IActionResult> CheckToDelete(int? id)
        {
            if (id is null)
                return NotFound();

            CategoryItem categoryitem = await _context.CategoryItems.FindAsync(id);

            if (categoryitem is null)
                return NotFound();

            return View(categoryitem);
        }

        public async Task<IActionResult> Delete([FromRoute] int? id)
        {
             CategoryItem categoryitem = await _context.CategoryItems.FindAsync(id);

            if (categoryitem is null)
                return NotFound();

            _context.CategoryItems.Remove(categoryitem);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return RedirectToAction(nameof(Index), new { categoryId = categoryitem.CategoryId });
        }
      
        private bool CategoryItemExists(int id)
        {
            return _context.CategoryItems.Any(e => e.Id == id);
        }
    }
}
