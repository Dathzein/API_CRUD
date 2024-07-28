using DataAcces;
using Models.Contracts;
using Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines
{
    public class Log: ILog 
    {
        private readonly BDContext _context;

        public Log(BDContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Metodo que guarda la traza de las excepciones
        /// </summary>
        /// <param name="idUsuario">Id usuario para registrar traza en log de error</param>
        /// <param name="metodo">Metodo en el que se genero la excepcion</param>
        /// <param name="ex">Excepcion para conocer cual fue la causa</param>
        public void RegistrarError(int idUsuario, string metodo, string ex)
        {
            try
            {
                Logs logError = new Logs
                {
                    id_usuario = idUsuario,
                    metodo = metodo,
                    excepcion = ex,
                    fecha_control = DateTime.Now
                };
                _context.Logs!.Add(logError);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
