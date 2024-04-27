using Hotel.ATR.Portal.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel.ATR.Portal
{
    public class HotelATRContext : DbContext
    {
        public  HotelATRContext(DbContextOptions<HotelATRContext> options) : base(options)
        {

        }

        public DbSet<Room> Rooms { get; set; }  

    }
}
