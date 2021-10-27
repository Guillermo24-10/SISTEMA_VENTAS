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

        public static DataTable BuscarCodigo(string valor)
        {
            DArticulos datos = new DArticulos(); // se instancia cuando llamas a una clase           
            return datos.BuscarCodigo(valor);
        }
        public static string Insertar(int idcategoria,string codigo,string nombre,decimal precio_venta,int stock,string descripcion,string imagen)
        {
            DArticulos datos = new DArticulos(); // se instancia cuando llamas a una clase

            string existe = datos.Existencia(nombre);
            if (existe.Equals("1"))
            {
                return "El articulo ya existe";
            }
            else
            {
                Articulo obj = new Articulo(); // instanciamos la clase categoria para los parametros
                obj.IdCategoria = idcategoria;
                obj.Codigo = codigo;
                obj.Nombre = nombre;
                obj.PrecioVenta = precio_venta;
                obj.Stock = stock;                
                obj.Descripcion = descripcion;
                obj.Imagen = imagen;
                return datos.Insertar(obj);
            }

        }
        public static string Actualizar(int id, int idcategoria, string codigo, string nombreAnt, string nombre, decimal precio_venta, int stock, string descripcion, string imagen)
        {
            DArticulos datos = new DArticulos(); // se instancia cuando llamas a una clase
            Articulo obj = new Articulo();

            if (nombreAnt.Equals(nombre))
            {
                obj.IdArticulo = id;
                obj.IdCategoria = idcategoria;
                obj.Codigo = codigo;
                obj.Nombre = nombre;
                obj.PrecioVenta = precio_venta;
                obj.Stock = stock;
                obj.Descripcion = descripcion;
                obj.Imagen = imagen;
                return datos.Actualizar(obj);
            }
            else
            {
                string existe = datos.Existencia(nombre);
                if (existe.Equals("1"))
                {
                    return "El articulo ya existe";
                }
                else
                {
                    // instanciamos la clase categoria para los parametros
                    obj.IdArticulo = id;
                    obj.IdCategoria = idcategoria;
                    obj.Codigo = codigo;
                    obj.Nombre = nombre;
                    obj.PrecioVenta = precio_venta;
                    obj.Stock = stock;
                    obj.Descripcion = descripcion;
                    obj.Imagen = imagen;
                    return datos.Actualizar(obj);
                }
            }




        }

        public static string Eliminar(int id)
        {
            DArticulos datos = new DArticulos(); // se instancia cuando llamas a una clase                      
            return datos.Eliminar(id);
        }
        public static string Activar(int id)
        {
            DArticulos datos = new DArticulos(); // se instancia cuando llamas a una clase                      
            return datos.Activar(id);
        }
        public static string Desactivar(int id)
        {
            DArticulos datos = new DArticulos(); // se instancia cuando llamas a una clase                      
            return datos.Desactivar(id);
        }
    }
}
