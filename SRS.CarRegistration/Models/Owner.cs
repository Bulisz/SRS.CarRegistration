using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRS.CarRegistration.Models;

[Table("Owners")]
public class Owner
{
    public int OwnerID { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public ICollection<Car> Cars { get; set; } = new List<Car>();
}
