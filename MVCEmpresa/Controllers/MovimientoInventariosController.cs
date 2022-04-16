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
    public class MovimientoInventariosController : Controller
    {
        private readonly EmpresaContext _context;

        public MovimientoInventariosController(EmpresaContext context)
        {
            _context = context;
        }

        // GET: MovimientoInventarios
        public async Task<IActionResult> Index()
        {
            var empresaContext = _context.MovimientoInventarios.Include(m => m.IdCatalogoInventarioNavigation).Include(m => m.IdOrdenNavigation).Include(m => m.IdTipoMovInventarioNavigation);
            return View(await empresaContext.ToListAsync());
        }

        // GET: MovimientoInventarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimientoInventario = await _context.MovimientoInventarios
                .Include(m => m.IdCatalogoInventarioNavigation)
                .Include(m => m.IdOrdenNavigation)
                .Include(m => m.IdTipoMovInventarioNavigation)
                .FirstOrDefaultAsync(m => m.IdMovimientoInventario == id);
            if (movimientoInventario == null)
            {
                return NotFound();
            }

            return View(movimientoInventario);
        }

        // GET: MovimientoInventarios/Create
        public IActionResult Create()
        {
            ViewData["IdCatalogoInventario"] = new SelectList(_context.CatalogoInventarios, "IdCatalogoInventario", "IdCatalogoInventario");
            ViewData["IdOrden"] = new SelectList(_context.Ordens, "IdOrden", "IdOrden");
            ViewData["IdTipoMovInventario"] = new SelectList(_context.TipoMovimientoInventarios, "IdTipoMovInventario", "IdTipoMovInventario");
            return View();
        }

        // POST: MovimientoInventarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMovimientoInventario,CodMovimientoInventario,IdCatalogoInventario,IdTipoMovInventario,IdOrden")] MovimientoInventario movimientoInventario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimientoInventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCatalogoInventario"] = new SelectList(_context.CatalogoInventarios, "IdCatalogoInventario", "IdCatalogoInventario", movimientoInventario.IdCatalogoInventario);
            ViewData["IdOrden"] = new SelectList(_context.Ordens, "IdOrden", "IdOrden", movimientoInventario.IdOrden);
            ViewData["IdTipoMovInventario"] = new SelectList(_context.TipoMovimientoInventarios, "IdTipoMovInventario", "IdTipoMovInventario", movimientoInventario.IdTipoMovInventario);
            return View(movimientoInventario);
        }

        // GET: MovimientoInventarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimientoInventario = await _context.MovimientoInventarios.FindAsync(id);
            if (movimientoInventario == null)
            {
                return NotFound();
            }
            ViewData["IdCatalogoInventario"] = new SelectList(_context.CatalogoInventarios, "IdCatalogoInventario", "IdCatalogoInventario", movimientoInventario.IdCatalogoInventario);
            ViewData["IdOrden"] = new SelectList(_context.Ordens, "IdOrden", "IdOrden", movimientoInventario.IdOrden);
            ViewData["IdTipoMovInventario"] = new SelectList(_context.TipoMovimientoInventarios, "IdTipoMovInventario", "IdTipoMovInventario", movimientoInventario.IdTipoMovInventario);
            return View(movimientoInventario);
        }

        // POST: MovimientoInventarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMovimientoInventario,CodMovimientoInventario,IdCatalogoInventario,IdTipoMovInventario,IdOrden")] MovimientoInventario movimientoInventario)
        {
            if (id != movimientoInventario.IdMovimientoInventario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimientoInventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimientoInventarioExists(movimientoInventario.IdMovimientoInventario))
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
            ViewData["IdCatalogoInventario"] = new SelectList(_context.CatalogoInventarios, "IdCatalogoInventario", "IdCatalogoInventario", movimientoInventario.IdCatalogoInventario);
            ViewData["IdOrden"] = new SelectList(_context.Ordens, "IdOrden", "IdOrden", movimientoInventario.IdOrden);
            ViewData["IdTipoMovInventario"] = new SelectList(_context.TipoMovimientoInventarios, "IdTipoMovInventario", "IdTipoMovInventario", movimientoInventario.IdTipoMovInventario);
            return View(movimientoInventario);
        }

        // GET: MovimientoInventarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimientoInventario = await _context.MovimientoInventarios
                .Include(m => m.IdCatalogoInventarioNavigation)
                .Include(m => m.IdOrdenNavigation)
                .Include(m => m.IdTipoMovInventarioNavigation)
                .FirstOrDefaultAsync(m => m.IdMovimientoInventario == id);
            if (movimientoInventario == null)
            {
                return NotFound();
            }

            return View(movimientoInventario);
        }

        // POST: MovimientoInventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimientoInventario = await _context.MovimientoInventarios.FindAsync(id);
            _context.MovimientoInventarios.Remove(movimientoInventario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimientoInventarioExists(int id)
        {
            return _context.MovimientoInventarios.Any(e => e.IdMovimientoInventario == id);
        }
    }
}
