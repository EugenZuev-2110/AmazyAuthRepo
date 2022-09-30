using Microsoft.EntityFrameworkCore;

namespace AmazyAuth.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=322398;database=amazyauthdb;",
                new MySqlServerVersion(new Version(8, 0, 30)));
        }
    }
}
