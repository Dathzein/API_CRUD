using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Data
{
    [Table("usuario")]
    public class Usuarios
    {
        [Key]
        public int id { get; set; }
        public string? usuario { get; set; }
        public string? contrasena { get; set; }
        public int codigo_identificacion { get; set; }
        public string? numero_identificacion { get; set; }
        public bool active { get; set; }
        public int id_role { get; set; }
        public DateTime fecha_control { get; set; }
        public DateTime fecha_actualizacion { get; set; }
    }
}
