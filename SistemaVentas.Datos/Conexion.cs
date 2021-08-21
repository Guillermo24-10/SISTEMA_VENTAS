using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Datos
{
    public class Conexion
    {
       
        private static Conexion con = null;

        private Conexion()
        {

        }
        public SqlConnection CrearConexion()
        {
            SqlConnection cadena = new SqlConnection();
            try
            {
                cadena.ConnectionString = "Data Source=SERVIDOR;Initial Catalog=DB_SISTEMA;User ID=sa;Password=sql";              
            }
            catch (Exception ex)
            {
                
                cadena = null;
                throw ex;
            }

            return cadena; 
        }

        public static Conexion getInstancia()
        {
            if (con == null)
            {
                con = new Conexion();
                
            }
            return con;
        }
        
    }
}
