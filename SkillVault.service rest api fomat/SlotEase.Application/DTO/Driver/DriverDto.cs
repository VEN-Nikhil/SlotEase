using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SlotEase.Application.DTO.Driver
{
    public class DriverDto
    {
        public long VendorId { get; set; } // From user->id
        public long DriverId { get; set; } // From user->id
    }
}