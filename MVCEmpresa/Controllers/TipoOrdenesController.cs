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
    public class TipoOrdenesController : Controller
    {
        private readonly EmpresaContext _context;

        public TipoOrdenesController(EmpresaContext context)
        {
            _context = context;
        }

        // GET: TipoOrdenes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoOrdens.ToListAsync());
        }

        // GET: TipoOrdenes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoOrden = await _context.TipoOrdens
                .FirstOrDefaultAsync(m => m.IdTipoOrden == id);
            if (tipoOrden == null)
            {
                return NotFound();
            }

            return View(tipoOrden);
        }

        // GET: TipoOrdenes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoOrdenes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoOrden,Nombre")] TipoOrden tipoOrden)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoOrden);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoOrden);
        }

        // GET: TipoOrdenes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoOrden = await _context.TipoOrdens.FindAsync(id);
            if (tipoOrden == null)
            {
                return NotFound();
            }
            return View(tipoOrden);
        }

        // POST: TipoOrdenes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoOrden,Nombre")] TipoOrden tipoOrden)
        {
            if (id != tipoOrden.IdTipoOrden)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoOrden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoOrdenExists(tipoOrden.IdTipoOrden))
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
            return View(tipoOrden);
        }

        // GET: TipoOrdenes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoOrden = await _context.TipoOrdens
                .FirstOrDefaultAsync(m => m.IdTipoOrden == id);
            if (tipoOrden == null)
            {
                return NotFound();
            }

            return View(tipoOrden);
        }

        // POST: TipoOrdenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoOrden = await _context.TipoOrdens.FindAsync(id);
            _context.TipoOrdens.Remove(tipoOrden);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoOrdenExists(int id)
        {
            return _context.TipoOrdens.Any(e => e.IdTipoOrden == id);
        }
    }
}
