using Microsoft.EntityFrameworkCore;
using Models.Data;
using Models.Data.Views;

namespace DataAcces
{
    public class BDontext: DbContext
    {
        public BDontext( DbContextOptions<BDontext> options):base(options) { }

        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Vw_Clientes> Vw_Clientes { get; set; }
    }
}
