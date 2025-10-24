using Microsoft.EntityFrameworkCore;
using telefon.Models;

namespace telefon.Models {
    public class Gvt : DbContext {
        public Gvt() { }
        public Gvt(DbContextOptions<Gvt> options) : base (options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Contacts.db");
        }

        public DbSet<Contacts> contacts { get; set; }
    }
}