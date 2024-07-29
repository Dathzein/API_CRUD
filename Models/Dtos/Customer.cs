using Models.Data;
using Models.Data.Views;
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
        [Required(ErrorMessage = "tipo de identificacion es requerido")]
        public int codigo_identificacion { get; set; }
        [Required(ErrorMessage = "Numero de identificacion es requerido")]
        public string? numero_identificacion { get; set; }
        [Required(ErrorMessage = "Correo es requerido")]
        [EmailAddress]
        public string? correo_electronico { get; set; }
        [Required(ErrorMessage = "Edad es requerida")]
        public int edad { get; set; }
        [Required(ErrorMessage = "Codigo de pais es requerido")]
        public int codigo_pais { get; set; }
        [Required(ErrorMessage = "Numero de telefono es requerido")]
        public string? numero_telefono { get; set; }
        [Required(ErrorMessage = "Estado es requerido")]
        public bool active { get; set; }
   
    }
    public class ResponseCustomer
    {
        public int idError { get; set; }
        public string? message { get; set; }
    }
    public class ResponseGetCustomers
    {
        public int idError { get; set;}
        public string? message { get; set; }
        public List<Vw_Clientes>? Clientes { get; set; }
        public Clientes? Cliente { get; set; }

    }
}
    
