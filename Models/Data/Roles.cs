using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Data
{
    [Table("roles")]
    public class Roles
    {
        [Key]
        public int id { get; set; }
        public string? role_nombre { get; set; }
        public bool active { get; set; }
        public DateTime fecha_control { get; set; }
        public DateTime fecha_actualizacion { get; set; }
    }
}
