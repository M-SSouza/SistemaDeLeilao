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
    public class LancesController : Controller
    {
        private readonly ProjectContext db;

        public LancesController(ProjectContext context)
        {
            db = context;
        }

        // GET: Lances
        public async Task<IActionResult> Index()
        {
            return View(await db.Lances.ToListAsync());
        }

        // GET: Lances/Create
        public IActionResult Create()
        {
            ViewBag.Pessoas = db.Pessoas.Select(p => new SelectListItem()
            { Text = p.Nome, Value = p.PessoasID.ToString() }).ToList();

            ViewBag.Produtos = db.Produtos.Select(p => new SelectListItem()
            { Text = p.Nome, Value = p.ProdutosID.ToString() }).ToList();

            return View();
        }

        // POST: Lances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LancesID,PessoasID,ProdutosID,Valor")] Lances lances)
        {

            //Procuro algum lance pertencente a esse produto que seja maior ao valor informado.
            bool valorMax = db.Lances.Where(x => x.ProdutosID == lances.ProdutosID).Any(x => x.Valor > lances.Valor); 
            if (valorMax)
            {
                ModelState.AddModelError("Valor", "O valor precisa ser maior ao último lance já feito.");
            }

            if (ModelState.IsValid)
            {
                db.Add(lances);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Pessoas = db.Pessoas.Select(p => new SelectListItem()
            { Text = p.Nome, Value = p.PessoasID.ToString() }).ToList();

            ViewBag.Produtos = db.Produtos.Select(p => new SelectListItem()
            { Text = p.Nome, Value = p.ProdutosID.ToString() }).ToList();
            return View(lances);
        }

        // GET: Lances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lances = await db.Lances.FindAsync(id);
            if (lances == null)
            {
                return NotFound();
            }
            return View(lances);
        }

        // POST: Lances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LancesID,PessoasID,ProdutosID,Valor")] Lances lances)
        {
            if (id != lances.LancesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(lances);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LancesExists(lances.LancesID))
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
            return View(lances);
        }

        // GET: Lances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lances = await db.Lances
                .FirstOrDefaultAsync(m => m.LancesID == id);
            if (lances == null)
            {
                return NotFound();
            }

            return View(lances);
        }

        // POST: Lances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lances = await db.Lances.FindAsync(id);
            db.Lances.Remove(lances);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LancesExists(int id)
        {
            return db.Lances.Any(e => e.LancesID == id);
        }
    }
}
