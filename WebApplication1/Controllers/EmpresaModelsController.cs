using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmpresaModelsController : Controller
    {
        private readonly WebApplication1Context _context;

        public EmpresaModelsController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: EmpresaModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmpresaModel.ToListAsync());
        }

        // GET: EmpresaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaModel = await _context.EmpresaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (empresaModel == null)
            {
                return NotFound();
            }

            return View(empresaModel);
        }

        // GET: EmpresaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpresaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,NomeFantasia,CNPJ,Situação")] EmpresaModel empresaModel)
        {
            //if (ModelState.IsValid)
            //{
            empresaModel.Situação = true;
                _context.Add(empresaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(empresaModel);
        }

        // GET: EmpresaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaModel = await _context.EmpresaModel.FindAsync(id);
            if (empresaModel == null)
            {
                return NotFound();
            }
            return View(empresaModel);
        }

        // POST: EmpresaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,NomeFantasia,CNPJ,Situação")] EmpresaModel empresaModel)
        {
            if (id != empresaModel.Codigo)
            {
                return NotFound();
            }

            if(empresaModel.Situação == false)
            {
                ModelState.AddModelError("Regra de negócio", "Não é possível editar uma empresa com a situação Inativa");
                return View(empresaModel);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaModelExists(empresaModel.Codigo))
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
            return View(empresaModel);
        }

        // GET: EmpresaModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaModel = await _context.EmpresaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (empresaModel == null)
            {
                return NotFound();
            }

            return View(empresaModel);
        }

        // POST: EmpresaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresaModel = await _context.EmpresaModel.FindAsync(id);
            if (empresaModel == null)
            {
                return NotFound();
            }
            if (empresaModel.Situação == true)
            {
                ModelState.AddModelError("Regra de negócio", "Não é possível excluir uma empresa com a situação Ativa");
                return View(empresaModel);
            }
            _context.EmpresaModel.Remove(empresaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaModelExists(int id)
        {
            return _context.EmpresaModel.Any(e => e.Codigo == id);
        }

        public async Task<IActionResult> AlterarStatus(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaModel = await _context.EmpresaModel.FindAsync(id);

            if (empresaModel == null)
            {
                return NotFound();
            }
            return await AlteraValor(id, empresaModel);

        }

        // POST: PessoaModels/AlteraValor/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AlteraValor(int id, EmpresaModel empresaModel)
        {
            if (id != empresaModel.Codigo)
            {
                return NotFound();
            }

            if (empresaModel.Situação == true)
            {
                empresaModel.Situação = false;
            }
            else
            {
                empresaModel.Situação = true;
            }
            try
            {
                _context.Update(empresaModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaModelExists(empresaModel.Codigo))
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
    }
}
