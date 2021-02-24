using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCDCollection.Data;
using MyCDCollection.Models;

namespace MyCDCollection.Controllers
{
    public class CDsController : Controller
    {
        private readonly MyCDCollectionContext _context;

        public CDsController(MyCDCollectionContext context)
        {
            _context = context;
        }

        // GET: CDs
        public async Task<IActionResult> Index(string searchName, string CDGenre)
        {

            var CD = _context.CD
           .Include(c => c.Artist)
           .Include(a => a.Name)
           .AsNoTracking();

            IQueryable<string> GenreQuery = from m in _context.CD
                                            orderby m.Genre
                                            select m.Genre;

             CD = from m in CD
                         select m;


            if (!string.IsNullOrEmpty(searchName))
            {
                CD = CD.Where(x => x.Album.Contains(searchName));
            }
            if (!string.IsNullOrEmpty(CDGenre))
            {
                CD = CD.Where(s => s.Genre == CDGenre);
            }
            var MusicGenreVM = new CDGenreVIewModel
            {
                Genre = new SelectList(await GenreQuery.Distinct().ToListAsync()),
                CDS = await CD.ToListAsync()
            };
            return View(MusicGenreVM);
        }

        // GET: CDs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cD = await _context.CD
                .FirstOrDefaultAsync(m => m.CDID == id);
            if (cD == null)
            {
                return NotFound();
            }

            return View(cD);
        }

        // GET: CDs/Create
        public async Task<IActionResult> Create(int id)
        {

            var CD = await _context.Artist
             .FirstOrDefaultAsync(m => m.ArtistID == id);
            if (CD == null)
            {
                return NotFound();
            }
            ViewBag.ArtistID = id;
            return View();
        }

        // POST: CDs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CDID ,Album,Genre,ReleaseDate,ArtistNameID, Lended")] CD cD)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cD);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cD);
        }

        // GET: CDs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cD = await _context.CD.FindAsync(id);
            if (cD == null)
            {
                return NotFound();
            }
            return View(cD);
        }

        // POST: CDs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CDID ,Album,Genre,ArtistNameID,Lended")] CD cD)
        {
            if (id != cD.CDID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cD);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CDExists(cD.CDID))
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
            return View(cD);
        }

        // GET: CDs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cD = await _context.CD
                .FirstOrDefaultAsync(m => m.CDID == id);
            if (cD == null)
            {
                return NotFound();
            }

            return View(cD);
        }

        // POST: CDs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cD = await _context.CD.FindAsync(id);
            _context.CD.Remove(cD);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CDExists(int id)
        {
            return _context.CD.Any(e => e.CDID == id);
        }
    }
}
