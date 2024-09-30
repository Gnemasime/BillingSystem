using BillingSystem.Data;
using BillingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class TariffController : Controller
{
    private readonly BillingContext _context;

    public TariffController(BillingContext context)
    {
        _context = context;
    }

    // GET: Tariff
    public async Task<IActionResult> Index()
    {
        var tariffs = await _context.Tariffs.ToListAsync();
        return View(tariffs);
    }

    // GET: Tariff/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Tariff/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Tariff tariff)
    {
        if (ModelState.IsValid)
        {
            _context.Tariffs.Add(tariff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tariff);
    }

    // GET: Tariff/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var tariff = await _context.Tariffs.FindAsync(id);
        if (tariff == null)
        {
            return NotFound();
        }
        return View(tariff);
    }

    // POST: Tariff/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Tariff tariff)
    {
        if (id != tariff.TariffId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(tariff);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TariffExists(tariff.TariffId))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(tariff);
    }

    // GET: Tariff/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var tariff = await _context.Tariffs.FindAsync(id);
        if (tariff == null)
        {
            return NotFound();
        }
        return View(tariff);
    }

    // POST: Tariff/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var tariff = await _context.Tariffs.FindAsync(id);
        _context.Tariffs.Remove(tariff);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TariffExists(int id)
    {
        return _context.Tariffs.Any(e => e.TariffId == id);
    }
}
