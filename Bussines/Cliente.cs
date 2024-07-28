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

        public Cliente( BDContext context )
        {
            _context = context;
        }

        public ResponseCustomer AddCustomer(RequestCustomer request)
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
                        fecha_control = DateTime.Now,
                        //fecha_actualizacion = 
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

                return new ResponseCustomer
                {
                    idError = -1,
                    message = "Ocurrrio un error al ejecutar esta accion"
                };
                //Crear una tabla que guarde el error
            }
        }
        public List<Vw_Clientes> GetCustomers()
        {
            try
            {
                List<Vw_Clientes> clientes = _context!.Vw_Clientes.Where(x=> x.Estado.Equals("Activo")).ToList();
                if( clientes.Count > 0 )   
                {
                    //retornar la lista
                    return clientes;
                }
                //nose hayaron registros
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //Modificar depornto pñara que consulte mas bien por documento y no id xdxd
        public Clientes GetCustomersById(string id)
        {
            try
            {
                Clientes? cliente = _context!.Clientes.Where(x => x.numero_identificacion == id).FirstOrDefault();
                if( cliente == null)
                {
                    //no se hayaron registros
                    return cliente;
                }

                //retorna cliente
                return cliente;


            }
            catch (Exception ex)
            {
                 return null;
                //Crear una tabla que guarde el error
            }
        }
        public ResponseCustomer UpdateCustomer(RequestCustomer request)
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

                return new ResponseCustomer
                {
                    idError = -1,
                    message = "Ocurrrio un error al ejecutar esta accion"
                };
                //Crear una tabla que guarde el error
            }
        }
        public ResponseCustomer DeleteCustomer(int id)
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

                return new ResponseCustomer
                {
                    idError = -1,
                    message = "Ocurrrio un error al ejecutar esta accion"
                };
                //Crear una tabla que guarde el error
            }
        }

    }
}
