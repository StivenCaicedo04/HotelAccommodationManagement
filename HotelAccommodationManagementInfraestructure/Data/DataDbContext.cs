
using HotelAccommodationManagementDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelAccommodationManagementInfrastructure.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) :  base(options) 
        { 
            
        }

        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<Passengers> Passengers { get; set; }
    }
}
