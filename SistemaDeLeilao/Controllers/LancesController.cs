using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return View(await db.Lances.Include(x => x.Produtos).Include(x => x.Pessoas).ToListAsync());
        }

        // GET: Lances/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Pessoas = await db.Pessoas.Select(p => new SelectListItem()
            { Text = p.Nome, Value = p.PessoasID.ToString() }).ToListAsync();

            ViewBag.Produtos = await db.Produtos.Select(p => new SelectListItem()
            { Text = p.Nome, Value = p.ProdutosID.ToString() }).ToListAsync();

            return View();
        }

        // POST: Lances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lances lances)
        {

            //Procuro algum lance pertencente a esse produto que seja maior ao valor informado.
            decimal? valorMax = db.Lances.Where(x => x.ProdutosID == lances.ProdutosID && x.Valor >= lances.Valor).Select(x => x.Valor).FirstOrDefault();

            
            //Caso exista um lance maior que essa tentativa ou o valor for abaixo do valor de lance inicial retornar um erro.
            if (valorMax != null)
            {
                ModelState.AddModelError("Valor", $"O valor precisa ser maior ao último lance já feito [{string.Format("{0:C}", valorMax)}].");
            }
            else
            {
                //Procuro o valor inicial do produto (quando não houver lance anterior).
                var produto = await db.Produtos.FindAsync(lances.ProdutosID);
                decimal? valorInicial = (produto != null) ? produto.Valor : null;
                if (lances.Valor < valorInicial)
                ModelState.AddModelError("Valor", $"O valor precisa ser igual ou maior que o valor inicial [{string.Format("{0:C}", valorInicial)}].");
            }

            if (ModelState.IsValid)
            {
                db.Add(lances);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Pessoas = await db.Pessoas.Select(p => new SelectListItem()
            { Text = p.Nome, Value = p.PessoasID.ToString() }).ToListAsync();

            ViewBag.Produtos = await db.Produtos.Select(p => new SelectListItem()
            { Text = p.Nome, Value = p.ProdutosID.ToString() }).ToListAsync();
            return View(lances);
        }

        // GET: Lances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lances = await db.Lances.Include(x => x.Pessoas).Include(x => x.Produtos).FirstOrDefaultAsync(x => x.LancesID == id);
            if (lances == null)
            {
                return NotFound();
            }

            ViewBag.Pessoas = await db.Pessoas.Select(p => new SelectListItem()
            { Text = p.Nome, Value = p.PessoasID.ToString() }).ToListAsync();

            ViewBag.Produtos = await db.Produtos.Select(p => new SelectListItem()
            { Text = p.Nome, Value = p.ProdutosID.ToString() }).ToListAsync();

            return View(lances);
        }

        // POST: Lances/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LancesID,PessoasID,ProdutosID,Valor")] Lances lances)
        {
            if (id != lances.LancesID)
            {
                return NotFound();
            }

            decimal? valorMax = db.Lances.Where(x => x.ProdutosID == lances.ProdutosID && x.Valor >= lances.Valor).Select(x => x.Valor).FirstOrDefault();


            //Caso exista um lance maior que essa tentativa ou o valor for abaixo do valor de lance inicial retornar um erro.
            if (valorMax != null)
            {
                ModelState.AddModelError("Valor", $"O valor precisa ser maior ao último lance já feito [{string.Format("{0:C}", valorMax)}].");
            }
            else
            {
                //Procuro o valor inicial do produto (quando não houver lance anterior).
                decimal? valorInicial = db.Produtos.FindAsync(lances.ProdutosID).Result.Valor;
                if (lances.Valor < valorInicial)
                    ModelState.AddModelError("Valor", $"O valor precisa ser igual ou maior que o valor inicial [{string.Format("{0:C}", valorInicial)}].");
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

            ViewBag.Pessoas = await db.Pessoas.Select(p => new SelectListItem()
            { Text = p.Nome, Value = p.PessoasID.ToString() }).ToListAsync();

            ViewBag.Produtos = await db.Produtos.Select(p => new SelectListItem()
            { Text = p.Nome, Value = p.ProdutosID.ToString() }).ToListAsync();

            return View(lances);
        }

        // GET: Lances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lances = await db.Lances.Include(x => x.Produtos).Include(x => x.Pessoas)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
