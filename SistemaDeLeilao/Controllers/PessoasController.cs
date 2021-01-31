using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaDeLeilao.Data;
using SistemaDeLeilao.Models;

namespace SistemaDeLeilao.Controllers
{
    public class PessoasController : Controller
    {
        private readonly ProjectContext db;

        public PessoasController(ProjectContext context)
        {
            db = context;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
            return View(await db.Pessoas.ToListAsync());
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoas = await db.Pessoas
                .FirstOrDefaultAsync(m => m.PessoasID == id);
            if (pessoas == null)
            {
                return NotFound();
            }

            return View(pessoas);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PessoasID,Nome,Idade")] Pessoas pessoas)
        {
            if (ModelState.IsValid)
            {
                db.Add(pessoas);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pessoas);
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoas = await db.Pessoas.FindAsync(id);
            if (pessoas == null)
            {
                return NotFound();
            }
            return View(pessoas);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PessoasID,Nome,Idade")] Pessoas pessoas)
        {
            if (id != pessoas.PessoasID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(pessoas);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoasExists(pessoas.PessoasID))
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
            return View(pessoas);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoas = await db.Pessoas
                .FirstOrDefaultAsync(m => m.PessoasID == id);
            if (pessoas == null)
            {
                return NotFound();
            }

            return View(pessoas);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoas = await db.Pessoas.FindAsync(id);
            db.Pessoas.Remove(pessoas);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoasExists(int id)
        {
            return db.Pessoas.Any(e => e.PessoasID == id);
        }
    }
}
