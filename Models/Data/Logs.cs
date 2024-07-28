using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Data
{
    [Table("logs")]
    public class Logs
    {
        [Key]
        public int id { get; set; }
        public int id_usuario { get; set; }
        public string? metodo { get; set; }
        public string? excepcion { get; set; }
        public DateTime fecha_control { get; set; }
    }
}
