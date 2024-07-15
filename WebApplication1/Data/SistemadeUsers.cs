using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using WebApplication1.Data.Map;

namespace WebApplication1.Data
{
    public class SistemadeUsers : DbContext
    {
        public SistemadeUsers(DbContextOptions<SistemadeUsers> options) : base(options) 
        {
        }

        public DbSet<User> Usuarios {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            modelBuilder.ApplyConfiguration(new UserMap());
        base.OnModelCreating(modelBuilder);
    }
    }
}
