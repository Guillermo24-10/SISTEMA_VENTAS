using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Entidades
{
    public class Ingreso
    {
        public int IdIngreso { get; set; }
        public int IdProveedor { get; set;}
        public int IdUsuario { get; set; }
        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public string NumComprobante { get; set; }
        public DateTime fecha { get; set; }
        public decimal impuesto { get; set; }
        public decimal Total { get; set; }
        public string estado { get; set; }
        public DataTable detalles { get; set; }

    }
}
