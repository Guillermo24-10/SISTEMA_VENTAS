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
   public class NUsuario
    {
        public static DataTable Listar()  //static para no instanciar en otras clase, solamente en esta clase
        {
            DUsuario datos = new DUsuario(); // se instancia cuando llamas a una clase
            return datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DUsuario datos = new DUsuario(); // se instancia cuando llamas a una clase           
            return datos.Buscar(valor);
        }

        public static DataTable Login(string email,string clave)
        {
            DUsuario datos = new DUsuario(); // se instancia cuando llamas a una clase           
            return datos.Login(email,clave);
        }

        public static string Insertar(int idrol, string nombre,string tipo_documento,string num_documento,string direccion,string telefono,string email,string clave)
        {
            DUsuario datos = new DUsuario(); // se instancia cuando llamas a una clase

            string existe = datos.Existencia(email);
            if (existe.Equals("1"))
            {
                return "El Usuario con el email ya existe";
            }
            else
            {
                Usuario obj = new Usuario(); // instanciamos la clase categoria para los parametros
                obj.IdRol = idrol;
                obj.nombre = nombre;
                obj.TipoDocumento = tipo_documento;
                obj.NumeroDocumento = num_documento;
                obj.Direccion = direccion;
                obj.Email = email;
                obj.Telefono = telefono;
                obj.Clave = clave;
                return datos.Insertar(obj);
            }

        }

        public static string Actualizar(int id, int idrol, string nombre, string tipo_documento, string num_documento, string direccion, string telefono,string emailAnt, string email, string clave)
        {
            DUsuario datos = new DUsuario(); // se instancia cuando llamas a una clase
            Usuario obj = new Usuario();

            if (emailAnt.Equals(email))
            {
                obj.IdUsuario = id;
                obj.IdRol = idrol;
                obj.nombre = nombre;
                obj.TipoDocumento = tipo_documento;
                obj.NumeroDocumento = num_documento;
                obj.Direccion = direccion;
                obj.Email = email;
                obj.Telefono = telefono;
                obj.Clave = clave;
                return datos.Actualizar(obj);
            }
            else
            {
                string existe = datos.Existencia(email);
                if (existe.Equals("1"))
                {
                    return "El Usuario con el email ya existe";
                }
                else
                {
                    // instanciamos la clase categoria para los parametros
                    obj.IdUsuario = id;
                    obj.IdRol = idrol;
                    obj.nombre = nombre;
                    obj.TipoDocumento = tipo_documento;
                    obj.NumeroDocumento = num_documento;
                    obj.Direccion = direccion;
                    obj.Email = email;
                    obj.Telefono = telefono;
                    obj.Clave = clave;
                    return datos.Actualizar(obj);
                }
            }




        }

        public static string Eliminar(int id)
        {
            DUsuario datos = new DUsuario(); // se instancia cuando llamas a una clase                      
            return datos.Eliminar(id);
        }
        public static string Activar(int id)
        {
            DUsuario datos = new DUsuario(); // se instancia cuando llamas a una clase                      
            return datos.Activar(id);
        }
        public static string Desactivar(int id)
        {
            DUsuario datos = new DUsuario(); // se instancia cuando llamas a una clase                      
            return datos.Desactivar(id);
        }
    }
}
