#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCEmpresa.Models;

namespace MVCEmpresa.Controllers
{
    public class TipoMovimientoInventariosController : Controller
    {
        private readonly EmpresaContext _context;

        public TipoMovimientoInventariosController(EmpresaContext context)
        {
            _context = context;
        }

        // GET: TipoMovimientoInventarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoMovimientoInventarios.ToListAsync());
        }

        // GET: TipoMovimientoInventarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMovimientoInventario = await _context.TipoMovimientoInventarios
                .FirstOrDefaultAsync(m => m.IdTipoMovInventario == id);
            if (tipoMovimientoInventario == null)
            {
                return NotFound();
            }

            return View(tipoMovimientoInventario);
        }

        // GET: TipoMovimientoInventarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoMovimientoInventarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoMovInventario,Nombre")] TipoMovimientoInventario tipoMovimientoInventario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoMovimientoInventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoMovimientoInventario);
        }

        // GET: TipoMovimientoInventarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMovimientoInventario = await _context.TipoMovimientoInventarios.FindAsync(id);
            if (tipoMovimientoInventario == null)
            {
                return NotFound();
            }
            return View(tipoMovimientoInventario);
        }

        // POST: TipoMovimientoInventarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoMovInventario,Nombre")] TipoMovimientoInventario tipoMovimientoInventario)
        {
            if (id != tipoMovimientoInventario.IdTipoMovInventario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoMovimientoInventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoMovimientoInventarioExists(tipoMovimientoInventario.IdTipoMovInventario))
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
            return View(tipoMovimientoInventario);
        }

        // GET: TipoMovimientoInventarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoMovimientoInventario = await _context.TipoMovimientoInventarios
                .FirstOrDefaultAsync(m => m.IdTipoMovInventario == id);
            if (tipoMovimientoInventario == null)
            {
                return NotFound();
            }

            return View(tipoMovimientoInventario);
        }

        // POST: TipoMovimientoInventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoMovimientoInventario = await _context.TipoMovimientoInventarios.FindAsync(id);
            _context.TipoMovimientoInventarios.Remove(tipoMovimientoInventario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoMovimientoInventarioExists(int id)
        {
            return _context.TipoMovimientoInventarios.Any(e => e.IdTipoMovInventario == id);
        }
    }
}
