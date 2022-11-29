using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using W24_TP2_Maxime_Caron.Models;
using W24_TP2_Maxime_Caron.ViewModels;

namespace W24_TP2_Maxime_Caron.Controllers
{
    public class SujetsController : Controller
    {
        private readonly forumContext _context;

        public SujetsController(forumContext context)
        {
            _context = context;
        }

        // GET: Sujets
        public async Task<IActionResult> Index(int? id)
        {

           var sujets = _context.Sujets
                .Where(s => s.CatId == id)
                .Select(s =>
            new viewSujet
            {
                SujetId = s.SujetId,
                SujetTitre = s.SujetTitre,
                SujetTexte = s.SujetTexte,
                MessageCount = s.Messages.Count(s => s.MsgActif),
                SujetVues = s.SujetVues,
                Utilisateur = s.UserNavigation.UserName,
                SujetDate = s.SujetDate,
                dateRecente = s.Messages.Max(m => m.MsgDate),
                User = s.User
            });

            return View(await sujets.ToListAsync());
        }

        // GET: Sujets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sujets == null)
            {
                return NotFound();
            }

            var sujet = await _context.Sujets
                .Include(s => s.Cat)
                .FirstOrDefaultAsync(m => m.SujetId == id);
            if (sujet == null)
            {
                return NotFound();
            }

            return View(sujet);
        }

        // GET: Sujets/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId");
            return View();
        }

        // POST: Sujets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("SujetId,CatId,Utilisateur,SujetTitre,SujetTexte,SujetDate,SujetVues,SujetActif")] Sujet sujet, int id)
        {
            if (ModelState.IsValid)
            {
                sujet.User = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(sujet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", sujet.CatId);
            return View(sujet);
        }

        // GET: Sujets/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sujets == null)
            {
                return NotFound();
            }

            var sujet = await _context.Sujets.FindAsync(id);
            if (sujet == null)
            {
                return NotFound();
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", sujet.CatId);
            return View(sujet);
        }

        // POST: Sujets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SujetId,CatId,Utilisateur,SujetTitre,SujetTexte,SujetDate,SujetVues,SujetActif")] Sujet sujet)
        {
            if (id != sujet.SujetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sujet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SujetExists(sujet.SujetId))
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
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", sujet.CatId);
            return View(sujet);
        }

        // GET: Sujets/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sujets == null)
            {
                return NotFound();
            }

            var sujet = await _context.Sujets
                .Include(s => s.Cat)
                .FirstOrDefaultAsync(m => m.SujetId == id);
            if (sujet == null)
            {
                return NotFound();
            }

            return View(sujet);
        }

        // POST: Sujets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sujets == null)
            {
                return Problem("Entity set 'ForumContext.Sujets'  is null.");
            }
            var sujet = await _context.Sujets.FindAsync(id);
            if (sujet != null)
            {
                _context.Sujets.Remove(sujet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SujetExists(int id)
        {
          return (_context.Sujets?.Any(e => e.SujetId == id)).GetValueOrDefault();
        }
    }
}
