﻿using DataAcces;
using Microsoft.EntityFrameworkCore;
using Models.Contracts;
using Models.Data;
using Models.Dtos;

namespace Bussines
{
    public class Cliente : ICliente
    {
        private readonly BDontext? _context;

        public Cliente( BDontext context )
        {
            _context = context;
        }

        public ResponseCustomer AddCustomer(RequestCustomer request)
        {
            try
            {
                if(request != null) 
                {
                    Clientes cliente = new Clientes
                    {
                        nombre = request.nombre,
                        codigo_identificacion = request.codigo_identificacion,
                        numero_identificacion = request.numero_identificacion,
                        correo_electronico = request.correo_electronico,
                        edad = request.edad,
                        codigo_pais = request.codigo_páis,
                        numero_telefono = request.numero_telefono,
                        active = request.active,
                        fecha_control = DateTime.Now,
                        //fecha_actualizacion = DateTime.Now
                    };
                      
              
                    _context!.Clientes.Add(cliente);
                    _context!.SaveChanges();

                    return new ResponseCustomer
                    {
                        idError = 0,
                        message = "Cliente se agrego con exito"
                    };
                    
                }
                else
                {
                    //Return string los campos estan incompletos
                    return new ResponseCustomer
                    {
                        idError = 1,
                        message = "Los campos deben estar completos"
                    };
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<Clientes> GetCustomers()
        {
            try
            {
                List<Clientes> clientes = _context!.Clientes.ToList();
                if( clientes.Count > 0 )
                {
                    //retornar la lista
                    return clientes;
                }
                else
                {
                    //nose hayaron registros
                    return clientes;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Clientes GetCustomersById(int id)
        {
            try
            {
                Clientes? cliente = _context!.Clientes.Where(x => x.Id == id).FirstOrDefault();
                if( cliente == null)
                {
                    //no se hayaron registros
                    return cliente;
                }
                else
                {
                    //retorna cliente
                    return cliente;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ResponseCustomer UpdateCustomer(RequestCustomer request)
        {
            try
            {
                if (request != null)
                {
                    Clientes? cliente = _context!.Clientes.Where(x => x.numero_identificacion == x.numero_identificacion).FirstOrDefault();
                    if(cliente != null)
                    {

                        cliente.nombre = request.nombre;
                        cliente.codigo_identificacion = request.codigo_identificacion;
                        cliente.numero_identificacion = request.numero_identificacion;
                        cliente.correo_electronico = request.correo_electronico;
                        cliente.edad = request.edad;
                        cliente.codigo_pais = request.codigo_páis;
                        cliente.numero_telefono = request.numero_telefono;
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

                throw;
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

                throw;
            }
        }

    }
}
