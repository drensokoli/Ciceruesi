using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ciceruesi.Data;
using Ciceruesi.Models;
using Microsoft.AspNetCore.Authorization;

namespace Ciceruesi.Controllers
{
    public class TweetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TweetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tweets
        public async Task<IActionResult> Index()
        {
              return _context.Tweet != null ? 
                          View(await _context.Tweet.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Tweet'  is null.");
        }

        // GET: Tweets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tweet == null)
            {
                return NotFound();
            }

            var tweet = await _context.Tweet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tweet == null)
            {
                return NotFound();
            }

            return View(tweet);
        }

        // GET: Tweets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tweets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,Created")] Tweet tweet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tweet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tweet);
        }

        // GET: Tweets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tweet == null)
            {
                return NotFound();
            }

            var tweet = await _context.Tweet.FindAsync(id);
            if (tweet == null)
            {
                return NotFound();
            }
            return View(tweet);
        }

        // POST: Tweets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,Created")] Tweet tweet)
        {
            if (id != tweet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tweet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TweetExists(tweet.Id))
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
            return View(tweet);
        }

        // GET: Tweets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tweet == null)
            {
                return NotFound();
            }

            var tweet = await _context.Tweet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tweet == null)
            {
                return NotFound();
            }

            return View(tweet);
        }

        // POST: Tweets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tweet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tweet'  is null.");
            }
            var tweet = await _context.Tweet.FindAsync(id);
            if (tweet != null)
            {
                _context.Tweet.Remove(tweet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TweetExists(int id)
        {
          return (_context.Tweet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
