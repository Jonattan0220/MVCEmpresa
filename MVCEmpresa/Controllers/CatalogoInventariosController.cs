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
    public class CatalogoInventariosController : Controller
    {
        private readonly EmpresaContext _context;

        public CatalogoInventariosController(EmpresaContext context)
        {
            _context = context;
        }

        // GET: CatalogoInventarios
        public async Task<IActionResult> Index()
        {
            var empresaContext = _context.CatalogoInventarios.Include(c => c.IdEstanteNavigation).Include(c => c.IdProductoNavigation).Include(c => c.IdUbicacionNavigation);
            return View(await empresaContext.ToListAsync());
        }

        // GET: CatalogoInventarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogoInventario = await _context.CatalogoInventarios
                .Include(c => c.IdEstanteNavigation)
                .Include(c => c.IdProductoNavigation)
                .Include(c => c.IdUbicacionNavigation)
                .FirstOrDefaultAsync(m => m.IdCatalogoInventario == id);
            if (catalogoInventario == null)
            {
                return NotFound();
            }

            return View(catalogoInventario);
        }

        // GET: CatalogoInventarios/Create
        public IActionResult Create()
        {
            ViewData["IdEstante"] = new SelectList(_context.Estantes, "IdEstante", "IdEstante");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            ViewData["IdUbicacion"] = new SelectList(_context.Ubicacions, "IdUbicacion", "IdUbicacion");
            return View();
        }

        // POST: CatalogoInventarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCatalogoInventario,CodInventario,Cantidad,IdProducto,IdUbicacion,IdEstante")] CatalogoInventario catalogoInventario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalogoInventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstante"] = new SelectList(_context.Estantes, "IdEstante", "IdEstante", catalogoInventario.IdEstante);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", catalogoInventario.IdProducto);
            ViewData["IdUbicacion"] = new SelectList(_context.Ubicacions, "IdUbicacion", "IdUbicacion", catalogoInventario.IdUbicacion);
            return View(catalogoInventario);
        }

        // GET: CatalogoInventarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogoInventario = await _context.CatalogoInventarios.FindAsync(id);
            if (catalogoInventario == null)
            {
                return NotFound();
            }
            ViewData["IdEstante"] = new SelectList(_context.Estantes, "IdEstante", "IdEstante", catalogoInventario.IdEstante);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", catalogoInventario.IdProducto);
            ViewData["IdUbicacion"] = new SelectList(_context.Ubicacions, "IdUbicacion", "IdUbicacion", catalogoInventario.IdUbicacion);
            return View(catalogoInventario);
        }

        // POST: CatalogoInventarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCatalogoInventario,CodInventario,Cantidad,IdProducto,IdUbicacion,IdEstante")] CatalogoInventario catalogoInventario)
        {
            if (id != catalogoInventario.IdCatalogoInventario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogoInventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogoInventarioExists(catalogoInventario.IdCatalogoInventario))
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
            ViewData["IdEstante"] = new SelectList(_context.Estantes, "IdEstante", "IdEstante", catalogoInventario.IdEstante);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", catalogoInventario.IdProducto);
            ViewData["IdUbicacion"] = new SelectList(_context.Ubicacions, "IdUbicacion", "IdUbicacion", catalogoInventario.IdUbicacion);
            return View(catalogoInventario);
        }

        // GET: CatalogoInventarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogoInventario = await _context.CatalogoInventarios
                .Include(c => c.IdEstanteNavigation)
                .Include(c => c.IdProductoNavigation)
                .Include(c => c.IdUbicacionNavigation)
                .FirstOrDefaultAsync(m => m.IdCatalogoInventario == id);
            if (catalogoInventario == null)
            {
                return NotFound();
            }

            return View(catalogoInventario);
        }

        // POST: CatalogoInventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogoInventario = await _context.CatalogoInventarios.FindAsync(id);
            _context.CatalogoInventarios.Remove(catalogoInventario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogoInventarioExists(int id)
        {
            return _context.CatalogoInventarios.Any(e => e.IdCatalogoInventario == id);
        }
    }
}
