using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Product.Models;
using WebAppProject.Data;

namespace WebAppProject.Controllers
{
    public class testproductsController : Controller
    {
        private readonly WebAppProjectContext _context;

        public testproductsController(WebAppProjectContext context)
        {
            _context = context;
        }

        // GET: testproducts
        public async Task<IActionResult> Index()
        {
              return _context.testproduct != null ? 
                          View(await _context.testproduct.ToListAsync()) :
                          Problem("Entity set 'WebAppProjectContext.testproduct'  is null.");
        }

        // GET: testproducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.testproduct == null)
            {
                return NotFound();
            }

            var testproduct = await _context.testproduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testproduct == null)
            {
                return NotFound();
            }

            return View(testproduct);
        }

        // GET: testproducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: testproducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,ProductType,Price")] testproduct testproduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testproduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testproduct);
        }

        // GET: testproducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.testproduct == null)
            {
                return NotFound();
            }

            var testproduct = await _context.testproduct.FindAsync(id);
            if (testproduct == null)
            {
                return NotFound();
            }
            return View(testproduct);
        }

        // POST: testproducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,ProductType,Price")] testproduct testproduct)
        {
            if (id != testproduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testproduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!testproductExists(testproduct.Id))
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
            return View(testproduct);
        }

        // GET: testproducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.testproduct == null)
            {
                return NotFound();
            }

            var testproduct = await _context.testproduct
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testproduct == null)
            {
                return NotFound();
            }

            return View(testproduct);
        }

        // POST: testproducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.testproduct == null)
            {
                return Problem("Entity set 'WebAppProjectContext.testproduct'  is null.");
            }
            var testproduct = await _context.testproduct.FindAsync(id);
            if (testproduct != null)
            {
                _context.testproduct.Remove(testproduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool testproductExists(int id)
        {
          return (_context.testproduct?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
