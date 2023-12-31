﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnMoreApp.Data;
using LearnMoreApp.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace LearnMoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContentController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Admin/Content/Create
        public IActionResult Create(int CategoryItemId,int CategoryId)
        {
            Content content = new Content
            {
                CategoryId= CategoryId,
                CatItemId=CategoryItemId
            };
            return View(content);
        }

        // POST: Admin/Content/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,HtmlContent,VideoLink,CatItemId,CategoryId")] Content content)
        {
            if (ModelState.IsValid)
            {
                content.CategoryItem = await _context.CategoryItems.FindAsync(content.CatItemId);
                _context.Add(content);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), "CategoryItem", new {categoryId=content.CategoryId});
            }
            return View(content);
        }

        // GET: Admin/Content/Update/5
        public async Task<IActionResult> Update(int CategoryItemId, int CategoryId)
        {
            if (CategoryItemId == 0)
            {
                return NotFound();
            }
            var content = await _context.Contents.SingleOrDefaultAsync(Item=>Item.CategoryItem.Id==CategoryItemId);
            content.CategoryId= CategoryId;
            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }

        // POST: Admin/Content/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Title,HtmlContent,VideoLink,CategoryId")] Content content)
        {
            if (id != content.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(content);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentExists(content.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "CategoryItem", new { categoryId = content.CategoryId });
            }
            return View(content);
        }

     
        private bool ContentExists(int id)
        {
            return _context.Contents.Any(e => e.Id == id);
        }
    }
}
