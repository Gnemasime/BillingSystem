using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillingSystem.Models
{
    public enum TariffName
{
    Residential,
    Commercial
}

public class Tariff
{
    public int TariffId { get; set; }
    public TariffName TariffType { get; set; } // Changed from string to enum
    public decimal Rate { get; set; }
}

}