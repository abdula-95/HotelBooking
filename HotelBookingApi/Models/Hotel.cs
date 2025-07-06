using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBookingApi.Models;

[Table("Hotels")]
public class Hotel
{
    [Key]
    public int Id { get; set; }

    public Guid Uuid { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}