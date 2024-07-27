using Microsoft.EntityFrameworkCore;
using Models.Data;

namespace DataAcces
{
    public class BDontext: DbContext
    {
        public BDontext( DbContextOptions<BDontext> options):base(options) { }

        public virtual DbSet<Clientes> Clientes { get; set; }
    }
}
