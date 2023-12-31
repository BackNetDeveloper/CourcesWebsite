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
    public class MediaTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MediaTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MediaType
        public async Task<IActionResult> Index()
        {
            return View(await _context.MediaTypes.ToListAsync());
        }

        // GET: Admin/MediaType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaType = await _context.MediaTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mediaType == null)
            {
                return NotFound();
            }

            return View(mediaType);
        }

        // GET: Admin/MediaType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MediaType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,ThumbnailImagePath")] MediaType mediaType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mediaType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mediaType);
        }

        // GET: Admin/MediaType/Edit/5
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaType = await _context.MediaTypes.FindAsync(id);
            if (mediaType == null)
            {
                return NotFound();
            }
            return View(mediaType);
        }

        // POST: Admin/MediaType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Title,ThumbnailImagePath")] MediaType mediaType)
        {
            if (id != mediaType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediaType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaTypeExists(mediaType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mediaType);
        }
        // GET: Admin/MediaType/Delete/5
        public async Task<IActionResult> CheckToDelete(int? id)
        {
            if (id is null)
                return NotFound();
            MediaType media = await _context.MediaTypes.FindAsync(id);
            if (media is null)
                return NotFound();
            return View(media);
        }

        // POST: Admin/MediaType/Delete/5
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var mediaType = await _context.MediaTypes.FindAsync(id);
            _context.MediaTypes.Remove(mediaType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaTypeExists(int id)
        {
            return _context.MediaTypes.Any(e => e.Id == id);
        }
    }
}
