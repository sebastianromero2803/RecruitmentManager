using Microsoft.EntityFrameworkCore;
using RecruitmentManager.Entities.Entities;

namespace RecruitmentManager.DataAccess.Context
{
    public class SqlServerContext : DbContext
    {
        private string _connectionString;
        public SqlServerContext()
        {
            _connectionString = "Data Source = DESKTOP-OMM0BK4\\SQLEXPRESS; Initial Catalog = RecruitmentManager; Integrated Security = true";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options) { }
        public DbSet<Client> Client { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey( c => new { c.IdClient } );
            modelBuilder.Entity<Client>().Property(c => c.IdClient).UseIdentityColumn()
                .Metadata.SetBeforeSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

            base.OnModelCreating(modelBuilder);
        }
    }
}
