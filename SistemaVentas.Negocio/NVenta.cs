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
    public class NVenta
    {
        public static DataTable Listar()  //static para no instanciar en otras clase, solamente en esta clase
        {
            DVenta datos = new DVenta(); // se instancia cuando llamas a una clase
            return datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DVenta datos = new DVenta(); // se instancia cuando llamas a una clase           
            return datos.Buscar(valor);
        }

        public static DataTable ListarDetalle(int id)
        {
            DVenta datos = new DVenta();// se instancia cuando llamas a una clase           
            return datos.ListarDetalle(id);
        }
        public static string Insertar(int idcliente,int idUsuario,string tipoComprobante,string serieComprobante,string numComprobante,decimal impuesto,decimal total,DataTable detalles)
        {
            DVenta datos = new DVenta(); // se instancia cuando llamas a una clase
            Venta obj = new Venta();

            obj.IdCliente = idcliente;
            obj.IdUsuario = idUsuario;
            obj.TipoComprobante = tipoComprobante;
            obj.SerieComprobante = serieComprobante;
            obj.NumComprobante = numComprobante;
            obj.impuesto = impuesto;
            obj.Total = total;
            obj.detalles = detalles;
            return datos.Insertar(obj);

        }

        public static string Anular(int id)
        {
            DVenta datos = new DVenta();
            return datos.Anular(id);
        }
    }
}
