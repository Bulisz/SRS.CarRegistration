using System.ComponentModel.DataAnnotations.Schema;

namespace SRS.CarRegistration.Models;

[Table("Cars")]
public class Car
{
    public int CarID { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string RegistrationNumber { get; set; } = string.Empty;
    public DateTime ProductionDate { get; set; }
    public ICollection<Owner> Owners { get; set; } = new List<Owner>();
}