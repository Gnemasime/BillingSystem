using System.ComponentModel.DataAnnotations;
using BillingSystem.Data;
using BillingSystem.Controllers;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSystem.Models
{
    public enum BillStatus
{
    Paid,
    Unpaid,
    Overdue
}

public class Bill
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BillId { get; set; }

    // Foreign Key Properties
    public int ServiceId { get; set; }
    public string UserId { get; set; }
    public int TariffId { get; set; }

    // Additional Properties
    public decimal AmountDue { get; set; }
    public decimal Usage { get; set; }
    public DateTime DueDate { get; set; }
    public BillStatus Status { get; set; }

    // Navigation Properties
    [ForeignKey("UserId")]
    public User User { get; set; }

    [ForeignKey("ServiceId")]
    public Service Service { get; set; }

    [ForeignKey("TariffId")]
    public Tariff Tariff { get; set; }

 // Method to get Tariff Rate
    public decimal TarriffRate(BillingContext context)
    {
        var rate = context.Tariffs
                .Where(r => r.TariffId == TariffId)
                .Select(r => r.Rate)
                .SingleOrDefault(); // Use SingleOrDefault to avoid exceptions if not found

            return rate;
        
    }
 // Method to get Service Rate 
//from the service table
    public decimal ServiceRate(BillingContext context)
    {
        var rate = context.Services
        .Where(r=> r.ServiceId == ServiceId)
        .Select(rate=>rate.Rate)
        .SingleOrDefault();
        return rate;
    }

//Total Amount Due
public decimal TotalAmounDue(BillingContext context)
{
    return AmountDue = (ServiceRate(context) * Usage) + (TarriffRate(context) * Usage) ;
}
}

}