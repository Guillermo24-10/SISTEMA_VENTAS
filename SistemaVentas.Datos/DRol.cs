using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Datos
{
    public class DRol
    {
        public DataTable Listar()
        {
            SqlDataReader resul;
            DataTable tabla = new DataTable();
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("sp_listar_rol", cn);//PROCEDURES
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open(); //abrimos la BD
                resul = cmd.ExecuteReader(); // almacenamos los datos en resul datareader
                tabla.Load(resul); // cargamos los datos del datareader a la datatable
                return tabla; //retornamos los datos 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open) //si la conexion esta abierta
                {
                    cn.Close();  // se cierra la conexion
                }
            }
        }
    }
}
