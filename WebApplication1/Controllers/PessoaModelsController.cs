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
    public class PessoaModelsController : Controller
    {
        private readonly WebApplication1Context _context;

        public PessoaModelsController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: PessoaModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PessoaModel.ToListAsync());
        }

        // GET: PessoaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // GET: PessoaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PessoaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //aqui eu coloquei a situação no bind
        public async Task<IActionResult> Create([Bind("Nome,Email,Codigo,QuantidadeFilhos,DataNascimento,Salario,Situação")] PessoaModel pessoaModel)
        {
            //if (ModelState.IsValid)
            //{
            pessoaModel.Situação = true;
            _context.Add(pessoaModel);

            //Parte de checagem de dados
            if (pessoaModel.QuantidadeFilhos < 0)
            {
                ModelState.AddModelError("", "A quantidade de filhos tem que ser maior ou igual a zero!");
                return View(pessoaModel);
            }
            if (pessoaModel.Salario < 1200 || pessoaModel.Salario > 13000)
            {
                ModelState.AddModelError("", "O salário tem que ser entre 1200 e 13000!");
                return View(pessoaModel);
            }

            var verifyEmail = await _context.PessoaModel.FirstOrDefaultAsync(m => m.Email == pessoaModel.Email);

            if (verifyEmail != null && verifyEmail.Codigo != pessoaModel.Codigo)
            {
                ModelState.AddModelError("", "Este email já está cadastrado!");
                return View(pessoaModel);
            }

            DateTime defaultDate = new DateTime(1990, 1, 1,00,00,00);
            if (pessoaModel.DataNascimento < defaultDate)
            {
                ModelState.AddModelError("", "A data de nascimento tem que ser depois de 01/01/1990!");
                return View(pessoaModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //return View(pessoaModel);
        }

        // GET: PessoaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel == null)
            {
                return NotFound();
            }
            return View(pessoaModel);
        }

        // POST: PessoaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //aqui eu coloquei a situação no bind
        public async Task<IActionResult> Edit(int? id, [Bind("Nome,Email,Codigo,QuantidadeFilhos,DataNascimento,Salario,Situação")] PessoaModel pessoaModel)
        {
            if (id != pessoaModel.Codigo)
            {
                return NotFound();
            }

            //Parte de checagem de dados
            if (pessoaModel.Situação == false)
            {
                ModelState.AddModelError("Regra de negócio", "Não é possível editar uma pessoa com a situação Inativa");
                return View(pessoaModel);
            }

            if (pessoaModel.QuantidadeFilhos < 0)
            {
                ModelState.AddModelError("Regra de negócio", "A quantidade de filhos tem que ser maior ou igual a zero!");
                return View(pessoaModel);
            }

            if (pessoaModel.Salario < 1200 || pessoaModel.Salario > 13000)
            {
                ModelState.AddModelError("Regra de negócio", "O salário tem que ser entre 1200 e 13000!");
                return View(pessoaModel);
            }

            var verifyEmail =  _context.PessoaModel.Where(m => m.Email.Equals(pessoaModel.Email) && m.Codigo != pessoaModel.Codigo);

            if (verifyEmail.Count() > 0)
            {
                ModelState.AddModelError("Regra de negócio", "Este email já está cadastrado!");
                return View(pessoaModel);
            }

            DateTime defaultDate = new DateTime(1990, 1, 1, 00, 00, 00);
            if (pessoaModel.DataNascimento < defaultDate)
            {
                ModelState.AddModelError("Regra de negócio", "A data de nascimento tem que ser depois de 01/01/1990!");
                return View(pessoaModel);
            }

            //Editando
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaModelExists(pessoaModel.Codigo))
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
            return View(pessoaModel);
        }

        // GET: PessoaModels/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.Codigo == id);

            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // POST: PessoaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            //Parte de checagem de dados
            if (pessoaModel.Situação == true)
            {
                ModelState.AddModelError("Regra de negócio", "Não é possível excluir uma pessoa com a situação Ativa");
                return View(pessoaModel);
            }

            _context.PessoaModel.Remove(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaModelExists(int id)
        {
            return _context.PessoaModel.Any(e => e.Codigo == id);
        }

        public async Task<IActionResult> AlterarStatus(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel.FindAsync(id);

            if (pessoaModel == null)
            {
                return NotFound();
            }
            return await AlteraValor(id, pessoaModel);

        }

        // POST: PessoaModels/AlteraValor/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AlteraValor(int id, PessoaModel pessoaModel)
        {
            if (id != pessoaModel.Codigo)
            {
                return NotFound();
            }

            if (pessoaModel.Situação == true)
            {
                pessoaModel.Situação = false;
            }
            else
            {
                pessoaModel.Situação = true;
            }
            try
            {
                _context.Update(pessoaModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaModelExists(pessoaModel.Codigo))
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
