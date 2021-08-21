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
    public class NCategoria
    {
    
        public static DataTable Listar()  //static para no instanciar en otras clase, solamente en esta clase
        {
            DCategoria datos = new DCategoria(); // se instancia cuando llamas a una clase
            return datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DCategoria datos = new DCategoria(); // se instancia cuando llamas a una clase           
            return datos.Buscar(valor);
        }

        public static string Insertar(string nombre,string descripcion)
        {
            DCategoria datos = new DCategoria(); // se instancia cuando llamas a una clase

            string existe = datos.Existencia(nombre);
            if (existe.Equals("1"))
            {
                return "La categoria ya existe";
            }
            else
            {
                Categoria obj = new Categoria(); // instanciamos la clase categoria para los parametros
                obj.Nombre = nombre;
                obj.Descripcion = descripcion;
                return datos.Insertar(obj);
            }
           
        }

        public static string Actualizar(int id, string nombreAnt, string nombre, string descripcion)
        {
            DCategoria datos = new DCategoria(); // se instancia cuando llamas a una clase
            Categoria obj = new Categoria();

            if (nombreAnt.Equals(nombre))
            {
                obj.IdCategoria = id;
                obj.Nombre = nombre;
                obj.Descripcion = descripcion;
                return datos.Actualizar(obj);
            }
            else
            {
                string existe = datos.Existencia(nombre);
                if (existe.Equals("1"))
                {
                    return "La categoria ya existe";
                }
                else
                {
                    // instanciamos la clase categoria para los parametros
                    obj.IdCategoria = id;
                    obj.Nombre = nombre;
                    obj.Descripcion = descripcion;
                    return datos.Actualizar(obj);
                }
            }
            

              
            
        }

        public static string Eliminar(int id)
        {
            DCategoria datos = new DCategoria(); // se instancia cuando llamas a una clase                      
            return datos.Eliminar(id);
        }
        public static string Activar(int id)
        {
            DCategoria datos = new DCategoria(); // se instancia cuando llamas a una clase                      
            return datos.Activar(id);
        }
        public static string Desactivar(int id)
        {
            DCategoria datos = new DCategoria(); // se instancia cuando llamas a una clase                      
            return datos.Desactivar(id);
        }
    }
}
