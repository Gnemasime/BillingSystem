using BillingSystem.Data;
using BillingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

public class ServiceController : Controller
{
    private readonly BillingContext _context;
     private readonly UserManager<User> _userManager; // Inject UserManager

    public ServiceController(BillingContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager; // Assign UserManager
    }

    // GET: Service
    public async Task<IActionResult> Index()
    {
         if (User.Identity.IsAuthenticated)
        {
            var Currentuser = await _userManager.GetUserAsync(User);
            ViewBag.Type = Currentuser?.Type; // Set the user type
        }

        var services = await _context.Services.ToListAsync();
        return View(services);
    }

    // GET: Service/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Service/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Service service)
    {
        if (ModelState.IsValid)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(service);
    }

    // GET: Service/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null)
        {
            return NotFound();
        }
        return View(service);
    }

    // POST: Service/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Service service)
    {
        if (id != service.ServiceId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(service);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(service.ServiceId))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(service);
    }

    // GET: Service/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null)
        {
            return NotFound();
        }
        return View(service);
    }

    // POST: Service/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var service = await _context.Services.FindAsync(id);
        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ServiceExists(int id)
    {
        return _context.Services.Any(e => e.ServiceId == id);
    }
}
