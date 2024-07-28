using Azure.Core;
using DataAcces;
using Microsoft.EntityFrameworkCore;
using Models.Contracts;
using Models.Data;
using Models.Data.Views;
using Models.Dtos;

namespace Bussines
{
    public class Cliente : ICliente
    {
        private readonly BDContext? _context;
        private readonly ILog _log;

        public Cliente( BDContext context, ILog log )
        {
            _context = context;
            _log = log;
        }

        /// <summary>
        /// Metodo que agrga un cliente nuevo a la base de datos
        /// </summary>
        /// <param name="request">Cliente nuevo a agregar</param>
        /// <param name="id">Id usuario para registrar traza en log de error</param>
        /// <returns></returns>
        public ResponseCustomer AddCustomer(RequestCustomer request, int id)
        {
            try
            {
                Clientes? cliente = _context!.Clientes.Where(x => x.numero_identificacion == request.numero_identificacion).FirstOrDefault();
                if (cliente == null)
                {
                    Clientes agregarCliente = new Clientes
                    {
                        nombre = request.nombre,
                        codigo_identificacion = request.codigo_identificacion,
                        numero_identificacion = request.numero_identificacion,
                        correo_electronico = request.correo_electronico,
                        edad = request.edad,
                        codigo_pais = request.codigo_pais,
                        numero_telefono = request.numero_telefono,
                        active = request.active,
                        fecha_control = DateTime.Now
                    };


                    _context!.Clientes.Add(agregarCliente);
                    _context!.SaveChanges();

                    return new ResponseCustomer
                    {
                        idError = 0,
                        message = "Cliente se agrego con exito"
                    };
                }
                else
                {
                    return new ResponseCustomer
                    {
                        idError = 2,
                        message = "El cliente ya existe"
                    };
                }

            }
            catch (Exception ex)
            {
                _log!.RegistrarError(id, "AddCostumer", ex.ToString());
                return new ResponseCustomer
                {
                    idError = -1,
                    message = "Ha ocurrido un error revisar log de error"
                };
                //Crear una tabla que guarde el error
            }
        }
        /// <summary>
        /// Metodo que consulta todos los clientes registrados
        /// </summary>
        /// <param name="id">Id usuario para registrar traza en log de error</param>
        /// <returns></returns>
        public ResponseGetCustomers GetCustomers(int id)
        {
            ResponseGetCustomers res = new ResponseGetCustomers();
            try
            {
                List<Vw_Clientes> clientes = _context!.Vw_Clientes.Where(x=> x.Estado.Equals("Activo")).ToList();
                if( clientes.Count > 0 )   
                {
                    res.idError = 0;
                    res.message = "Proceso exitoso.";
                    res.Cliente = null;
                    res.Clientes = clientes;
                    //no se hayaron registros
                    return res;
                }
                //nose hayaron registros
                return res;
            }
            catch (Exception ex)
            {
                _log!.RegistrarError(id, "GetCustomers", ex.ToString());
                res.idError = -1;
                res.message = "Ha ocurrido un error revisar log de error";
                return res;
            }
        }
        /// <summary>
        /// Metodo para Consultar un cliente en especifico
        /// </summary>
        /// <param name="id"> Numero de identificacion para buscar el cliente</param>
        /// <param name="idUser">Id usuario para registrar traza en log de error</param>
        /// <returns></returns>
        public ResponseGetCustomers GetCustomersById(string id, int idUser)
        {
            ResponseGetCustomers res = new ResponseGetCustomers();
            try
            {
                Clientes? cliente = _context!.Clientes.Where(x => x.numero_identificacion == id).FirstOrDefault();
                if( cliente != null)
                {
                    res.idError = 0;
                    res.message = "Proceso exitoso.";
                    res.Cliente = cliente;
                    res.Clientes = null;
                    //no se hayaron registros
                    return res;
                }

                //retorna cliente
                return res;


            }
            catch (Exception ex)
            {
                _log!.RegistrarError(idUser, "GetCustomers", ex.ToString());
                res.idError = -1;
                res.message = "Ha ocurrido un error revisar log de error";
                return res;
                //Crear una tabla que guarde el error
            }
        }
        /// <summary>
        /// Metodo para actualizar un cliente
        /// </summary>
        /// <param name="request">Datos del cliente que se va actualizar</param>
        /// <param name="id">Id usuario para registrar traza en log de error</param>
        /// <returns></returns>
        public ResponseCustomer UpdateCustomer(RequestCustomer request, int id)
        {
            try
            {
                if (request != null)
                {
                    Clientes? cliente = _context!.Clientes.Where(x => x.numero_identificacion == request.numero_identificacion).FirstOrDefault();
                    if(cliente != null)
                    {

                        cliente.nombre = !string.IsNullOrEmpty(request.nombre) ? request.nombre : cliente.nombre;
                        cliente.codigo_identificacion = request.codigo_identificacion < 0 ? cliente.codigo_identificacion : request.codigo_identificacion;
                        cliente.numero_identificacion = !string.IsNullOrEmpty(request.numero_identificacion) ? request.numero_identificacion : cliente.numero_identificacion;
                        cliente.correo_electronico = !string.IsNullOrEmpty(request.correo_electronico) ? request.correo_electronico : cliente.correo_electronico;
                        cliente.edad = request.edad < 0 ? cliente.edad : request.edad;
                        cliente.codigo_pais = request.codigo_pais < 0 ? cliente.codigo_pais: request.codigo_pais;
                        cliente.numero_telefono = !string.IsNullOrEmpty(request.numero_telefono) ? request.numero_telefono : cliente.numero_telefono;
                        cliente.active = request.active;
                        cliente.fecha_actualizacion = DateTime.Now;
                        
                        _context!.Entry(cliente).State = EntityState.Modified;
                        _context!.SaveChanges();
                    }
                    return new ResponseCustomer
                    {
                        idError = 0,
                        message = "Cliente se actualizado con exito"
                    };

                }
                else
                {
                    //Return string los campos estan incompletos

                    return new ResponseCustomer
                    {
                        idError = 1,
                        message = "Los campos estan incompletos"
                    };
                }
            }
            catch (Exception ex)
            {
                _log!.RegistrarError(id, "UpdateCostumer", ex.ToString());
                return new ResponseCustomer
                {
                    idError = -1,
                    message = "Ha ocurrido un error revisar log de error"
                };
                //Crear una tabla que guarde el error
            }
        }
        /// <summary>
        /// Metodo para eliminar un cliente
        /// </summary>
        /// <param name="id">Id de usuario para eliminar cliente</param>
        /// <param name="idUser">Id usuario para registrar traza en log de error</param>
        /// <returns></returns>
        public ResponseCustomer DeleteCustomer(int id, int idUser)
        {
            try
            {
                Clientes? cliente = _context!.Clientes.Where(x => x.Id == id).FirstOrDefault();
                if (cliente == null)
                {
                    //no se hayaron registros
                    return new ResponseCustomer
                    {
                        idError = 1,
                        message = "No se encontro el cliente"
                    };
                }
                else
                {
                    _context.Clientes.Remove(cliente);
                    _context!.SaveChanges();
                    return new ResponseCustomer
                    {
                        idError = 0,
                        message = "Cliente se elimino con exito"
                    };
                }
            }
            catch (Exception ex)
            {
                _log!.RegistrarError(idUser, "DeleteCostumer", ex.ToString());
                return new ResponseCustomer
                {
                    idError = -1,
                    message = "Ha ocurrido un error revisar log de error"
                };
                //Crear una tabla que guarde el error
            }
        }

    }
}
