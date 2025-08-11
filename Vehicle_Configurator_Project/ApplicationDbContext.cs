using Microsoft.EntityFrameworkCore;
using Vehicle_Configurator_Project.Models;

namespace Vehicle_Configurator_Project
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Model> Models { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<VehicleDetail> VehicleDetails { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<SegmentManufacturerAssociation> SegmentManufacturerAssociations { get; set; }
        public DbSet<AlternateComponent> AlternateComponents { get; set; }
    }

}
