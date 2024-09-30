using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BillingSystem.Models
{
    public class User : IdentityUser
{
    [Key]
   
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public string CellPhone { get; set; }
    public string IdNumber {get;set;}
    public DateTime DateOfBirth { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }

    public string Type { get; set; } // Make sure this is set when registering
    
 
}

}