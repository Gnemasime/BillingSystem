using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BillingSystem.Models;
using BillingSystem.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

public class BillController : Controller
{
    private readonly BillingContext _context;

    public BillController(BillingContext context)
    {
        _context = context;
    }

    // GET: Bill
    public async Task<IActionResult> Index()
    {
        var bills = await _context.Bills
            .Include(b => b.Service)
            .Include(b => b.User)
            .Include(b => b.Tariff)
            .ToListAsync();
        return View(bills);
    }

    // GET: Bill/Create
    public IActionResult Create()
    {
        ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId");
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
        ViewData["TariffId"] = new SelectList(_context.Tariffs, "TariffId", "TariffId");
        return View();
    }

    // POST: Bill/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Bill bill)
    {
        if (ModelState.IsValid)
        {
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceId", bill.ServiceId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", bill.UserId);
        ViewData["TariffId"] = new SelectList(_context.Tariffs, "TariffId", "TariffId", bill.TariffId);
        return View(bill);
    }

    // GET: Bill/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var bill = await _context.Bills.FindAsync(id);
        if (bill == null)
        {
            return NotFound();
        }
        ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceType", bill.ServiceId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "FirstName", bill.UserId);
        ViewData["TariffId"] = new SelectList(_context.Tariffs, "TariffId", "TariffType", bill.TariffId);
        return View(bill);
    }

    // POST: Bill/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Bill bill)
    {
        if (id != bill.BillId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(bill);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(bill.BillId))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["ServiceId"] = new SelectList(_context.Services, "ServiceId", "ServiceType", bill.ServiceId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "FirstName", bill.UserId);
        ViewData["TariffId"] = new SelectList(_context.Tariffs, "TariffId", "TariffType", bill.TariffId);
        return View(bill);
    }

    // GET: Bill/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var bill = await _context.Bills
            .Include(b => b.Service)
            .Include(b => b.User)
            .Include(b => b.Tariff)
            .FirstOrDefaultAsync(m => m.BillId == id);
        if (bill == null)
        {
            return NotFound();
        }
        return View(bill);
    }

    // POST: Bill/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var bill = await _context.Bills.FindAsync(id);
        _context.Bills.Remove(bill);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BillExists(int id)
    {
        return _context.Bills.Any(e => e.BillId == id);
    }
}
