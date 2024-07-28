using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Data.Views
{
    [Table("vw_clientes")]
    public class Vw_Clientes
    {
        [Key]
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? tipo_identificacion { get; set; }
        public string? numero_identificacion { get; set; }
        public string? correo_electronico { get; set; }
        public int edad { get; set; }
        public string? pais { get; set; }
        public string? numero_telefono { get; set; }
        public string? Estado { get; set; }
    }
}
