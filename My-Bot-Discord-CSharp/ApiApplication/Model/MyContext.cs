using BusClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace ApiApplication.Model
{
    public class MyContext : DbContext
    {

        private string ConnectionString { get; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=BDProjectStock;Trusted_Connection=True;MultipleActiveResultSets=true";


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);

            object p = optionsBuilder.UseSqlServer(ConnectionString);
        }




        public override DbSet<TEntity> Set<TEntity>()
        {

            return base.Set<TEntity>();
        }

        public DbSet<Bus> _bus { get; set; }

      
    }
}
