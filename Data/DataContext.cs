using Microsoft.EntityFrameworkCore;
using PraticeSessionDemo.Models;

namespace PraticeSessionDemo.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    //optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB; database=StduentsData; trusted_connection=true;");
        //}
            
        public DbSet<Student> Students { get; set; }

    }
}
