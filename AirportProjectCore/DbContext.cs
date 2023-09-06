using AirportProjectCore.Models;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace AirportProjectCore
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opts) : base(opts) { }



        public DbSet<AirportInfo> airportinfo { get; set; }
        public DbSet<CityInfo> cityInfo { get; set; }
        public DbSet<FeedBack> FeedBack { get; set; }
        public DbSet<StateImg> StateImg { get; set; }

    }
}
