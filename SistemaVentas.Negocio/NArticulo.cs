using SistemaVentas.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Negocio
{
    public class NArticulo
    {
        public static DataTable Listar()  //static para no instanciar en otras clase, solamente en esta clase
        {
            DArticulos datos = new DArticulos(); // se instancia cuando llamas a una clase
            return datos.Listar();
        }
        public static DataTable Buscar(string valor)
        {
            DArticulos datos = new DArticulos(); // se instancia cuando llamas a una clase           
            return datos.Buscar(valor);
        }
        public static string Insertar(string nombre, string descripcion)
        {
            DArticulos datos = new DArticulos(); // se instancia cuando llamas a una clase

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
    }
}
