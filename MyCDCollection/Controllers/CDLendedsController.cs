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
    public class CDLendedsController : Controller
    {
        private readonly MyCDCollectionContext _context;

        public CDLendedsController(MyCDCollectionContext context)
        {
            _context = context;
        }

        // GET: CDLendeds
        public async Task<IActionResult> Index()
        {
         

            var LendedCDS = _context.LendedCDS
                .Include(c => c.CDs)
                .AsNoTracking();
            return View(await LendedCDS.ToListAsync());
        }

        // GET: CDLendeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cDLended = await _context.LendedCDS
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (cDLended == null)
            {
                return NotFound();
            }

            return View(cDLended);
        }

        // GET: CDLendeds/Create
        public async Task<IActionResult> Create(int id)
        {
            var cDLended = await _context.CD
               .FirstOrDefaultAsync(m => m.CDID == id);
            if (cDLended == null)
            {
                return NotFound();
            }
            ViewBag.CDID = id;
            return View();
        }

        // POST: CDLendeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonID,CDSID,Name,SurName,Lended")] CDLended cDLended)
        {
            if (ModelState.IsValid)
            {
                var linq = _context.CD.SingleOrDefault(x => x.CDID == cDLended.CDSID);
                cDLended.CDs = linq;
                _context.Add(cDLended);
                linq.Lended = true;
                _context.Update(linq);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cDLended);
        }

        // GET: CDLendeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cDLended = await _context.LendedCDS.FindAsync(id);
            if (cDLended == null)
            {
                return NotFound();
            }
            return View(cDLended);
        }

        // POST: CDLendeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonID,CDSID,Name,SurName,Lended")] CDLended cDLended)
        {
            if (id != cDLended.PersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cDLended);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CDLendedExists(cDLended.PersonID))
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
            return View(cDLended);
        }

        // GET: CDLendeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cDLended = await _context.LendedCDS
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (cDLended == null)
            {
                return NotFound();
            }

            return View(cDLended);
        }

        // POST: CDLendeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cDLended = await _context.LendedCDS.FindAsync(id);
            var CD = await _context.CD.SingleOrDefaultAsync(x => x.CDID == cDLended.CDSID);
            CD.Lended = false;
            _context.LendedCDS.Remove(cDLended);
            _context.CD.Update(CD);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CDLendedExists(int id)
        {
            return _context.LendedCDS.Any(e => e.PersonID == id);
        }
    }
}
