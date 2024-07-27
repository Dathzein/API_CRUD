using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class RequestCustomer
    {
        [Required(ErrorMessage = "Nombre es requerido")]
        //[StringLength(255,MinimumLength =1, ErrorMessage ="No puede estar vacio")]
        public string? nombre { get; set; }
        public int codigo_identificacion { get; set; }
        public string? numero_identificacion { get; set; }
        public string? correo_electronico { get; set; }
        public int edad { get; set; }
        public int codigo_pais { get; set; }
        public string? numero_telefono { get; set; }
        public bool active { get; set; }
   
    }
    public class ResponseCustomer
    {
        public int idError { get; set; }
        public string? message { get; set; }
    }
}
    
