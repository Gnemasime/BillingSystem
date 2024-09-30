using Microsoft.AspNetCore.Identity;

namespace BillingSystem.Models
{
    public class UserDashboard
{
    public IdentityUser User { get; set; }
    public List<Bill> Bills { get; set; }
}

}