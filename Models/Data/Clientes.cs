using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Data
{
    [Table("clientes")]
    public class Clientes
    {
        [Key]
        public int Id { get; set; }
        public string? nombre { get; set; }
        public int codigo_identificacion { get; set; }
        public string? numero_identificacion { get; set; }
        public string? correo_electronico { get; set; }
        public int edad {  get; set; }
        public int codigo_pais { get; set; }
        public string? numero_telefono { get; set; }
        public bool active { get; set; }
        public DateTime fecha_control { get; set; }
        public DateTime fecha_actualizacion { get; set; }
    }
}
