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
