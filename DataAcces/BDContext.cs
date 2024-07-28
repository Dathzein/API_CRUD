using Microsoft.EntityFrameworkCore;
using Models.Data;
using Models.Data.Views;

namespace DataAcces
{
    public class BDContext: DbContext
    {
        public BDContext( DbContextOptions<BDContext> options):base(options) { }

        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Vw_Clientes> Vw_Clientes { get; set; }
    }
}
