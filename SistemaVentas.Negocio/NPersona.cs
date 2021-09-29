using SistemaVentas.Datos;
using SistemaVentas.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Negocio
{
    public class NPersona
    {
        public static DataTable Listar()  //static para no instanciar en otras clase, solamente en esta clase
        {
            DPersona datos = new DPersona(); // se instancia cuando llamas a una clase
            return datos.Listar();
        }

        public static DataTable ListarProveedores()  //static para no instanciar en otras clase, solamente en esta clase
        {
            DPersona datos = new DPersona(); // se instancia cuando llamas a una clase
            return datos.ListarProveedores();
        }
        public static DataTable ListarClientes()  //static para no instanciar en otras clase, solamente en esta clase
        {
            DPersona datos = new DPersona(); // se instancia cuando llamas a una clase
            return datos.ListarClientes();
        }

        public static DataTable Buscar(string valor)
        {
            DPersona datos = new DPersona(); // se instancia cuando llamas a una clase           
            return datos.Buscar(valor);
        }
        public static DataTable BuscarProveedores(string valor)
        {
            DPersona datos = new DPersona(); // se instancia cuando llamas a una clase           
            return datos.BuscarProveedores(valor);
        }
        public static DataTable BuscarClientes(string valor)
        {
            DPersona datos = new DPersona(); // se instancia cuando llamas a una clase           
            return datos.BuscarClientes(valor);
        }

        public static string Insertar(string tipo_persona, string nombre, string tipo_documento, string num_documento, string direccion, string telefono, string email)
        {
            DPersona datos = new DPersona(); // se instancia cuando llamas a una clase

            string existe = datos.Existencia(nombre);
            if (existe.Equals("1"))
            {
                return "La persona ya se encuentra registrada";
            }
            else
            {
                Persona obj = new Persona(); // instanciamos la clase categoria para los parametros
                obj.TipoPersona = tipo_persona;
                obj.nombre = nombre;
                obj.tipoDocumento= tipo_documento;
                obj.numDocumento = num_documento;
                obj.direccion = direccion;
                obj.email = email;
                obj.telefono = telefono;               
                return datos.Insertar(obj);
            }

        }
        public static string Actualizar(int id, string tipo_persona,string nombreAnt, string nombre, string tipo_documento, string num_documento, string direccion, string telefono, string email)
        {
            DPersona datos = new DPersona(); // se instancia cuando llamas a una clase
            Persona obj = new Persona();

            if (nombreAnt.Equals(nombre))
            {
                obj.IdPersona = id;
                obj.TipoPersona =tipo_persona;          
                obj.nombre = nombre;
                obj.tipoDocumento = tipo_documento;
                obj.numDocumento = num_documento;
                obj.direccion = direccion;
                obj.telefono = telefono;
                obj.email = email;
                          
                return datos.Actualizar(obj);
            }
            else
            {
                string existe = datos.Existencia(nombre);
                if (existe.Equals("1"))
                {
                    return "La persona ya se encuentra registrada";
                }
                else
                {
                    // instanciamos la clase categoria para los parametros
                    obj.IdPersona = id;
                    obj.TipoPersona = tipo_persona;
                    obj.nombre = nombre;
                    obj.tipoDocumento = tipo_documento;
                    obj.numDocumento = num_documento;
                    obj.direccion = direccion;
                    obj.telefono = telefono;
                    obj.email = email;
                    return datos.Actualizar(obj);
                }
            }




        }
        public static string Eliminar(int id)
        {
            DPersona datos = new DPersona(); // se instancia cuando llamas a una clase                      
            return datos.Eliminar(id);
        }
    }
}
