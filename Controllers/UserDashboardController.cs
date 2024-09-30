using BillingSystem.Data;
using BillingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

public class UserDashboardController : Controller
{
    private readonly UserManager<User> _userManager; // Use your custom User class
    private readonly BillingContext _context;

    // Correctly inject UserManager<User> into the constructor
    public UserDashboardController(UserManager<User> userManager, BillingContext context)
    {
        _userManager = userManager; // Correctly initialize the _userManager field
        _context = context;
    }

    // GET: User/Details
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        // Fetch user bills
        var bills = _context.Bills.Where(b => b.UserId == user.Id).ToList();

        var model = new UserDashboard
        {
            User = user,
            Bills = bills
        };

        return View(model);
    }
}
