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
using W24_TP2_Maxime_Caron.Tools;
using W24_TP2_Maxime_Caron.ViewModels;

namespace W24_TP2_Maxime_Caron.Controllers
{
    public class MessagesController : Controller
    {
        private readonly forumContext _context;

        public MessagesController(forumContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Index(int? id, int? pageNumber, int nbrMessage)
        {
            var nbrMessagePage = 0;
            if (nbrMessage > 0)
            {
                nbrMessagePage = nbrMessage;
            }
            else
            {
                nbrMessagePage = 10;
            }
            var messages = _context.Messages.Where(m => m.SujetId == id)
                                                .Include(m => m.Sujet)
                                                .Select(m =>
                                                new Message
                                                {
                                                    MsgId = m.MsgId,
                                                    Utilisateur = m.UserNavigation.UserName,
                                                    MsgText = m.MsgText,
                                                    MsgDate = m.MsgDate,
                                                    User = m.User
                                                });
            return View(await PaginatedList<Message>.CreateAsync(messages, pageNumber ?? 1, nbrMessagePage));
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Sujet)
                .FirstOrDefaultAsync(m => m.MsgId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["SujetId"] = new SelectList(_context.Sujets, "SujetId", "SujetId");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("MsgId,SujetId,Utilisateur,MsgText,MsgDate,MsgActif")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.User = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SujetId"] = new SelectList(_context.Sujets, "SujetId", "SujetId", message.SujetId);
            return View(message);
        }

        // GET: Messages/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            ViewData["SujetId"] = new SelectList(_context.Sujets, "SujetId", "SujetId", message.SujetId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MsgId,SujetId,Utilisateur,MsgText,MsgDate,MsgActif")] Message message)
        {
            if (id != message.MsgId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.MsgId))
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
            ViewData["SujetId"] = new SelectList(_context.Sujets, "SujetId", "SujetId", message.SujetId);
            return View(message);
        }

        // GET: Messages/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Sujet)
                .FirstOrDefaultAsync(m => m.MsgId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Messages == null)
            {
                return Problem("Entity set 'ForumContext.Messages'  is null.");
            }
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
          return (_context.Messages?.Any(e => e.MsgId == id)).GetValueOrDefault();
        }
    }
}
