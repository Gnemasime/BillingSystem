using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSystem.Models
{
    public enum ServiceType
{
    Electricity,
    Water
}

public class Service
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ServiceId { get; set; }
    public ServiceType ServiceType { get; set; }
    public decimal Rate { get; set; }
}

}