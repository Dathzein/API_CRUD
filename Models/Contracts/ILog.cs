using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Contracts
{
    public interface ILog
    {
        void RegistrarError(int idUsuario, string metodo, string ex);
    }
}
