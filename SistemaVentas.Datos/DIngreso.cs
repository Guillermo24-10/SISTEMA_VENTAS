using SistemaVentas.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Datos
{
    public class DIngreso
    {
        public DataTable Listar()
        {
            SqlDataReader resul;
            DataTable tabla = new DataTable();
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("sp_ingreso_listar", cn);//PROCEDURES
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

        public DataTable Buscar(string valor)
        {
            SqlDataReader resul;
            DataTable tabla = new DataTable();
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("sp_ingreso_buscar", cn);//PROCEDURES
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor; // se agrega los parametros del procedure
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

        public DataTable ListarDetalle(int id)
        {
            SqlDataReader resul;
            DataTable tabla = new DataTable();
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("sp_ingreso_listar_detalle", cn);//PROCEDURES
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idingreso", SqlDbType.VarChar).Value = id; // se agrega los parametros del procedure
                cn.Open(); //abrimos la BD
                resul = cmd.ExecuteReader(); // almacenamos los datos en resul datareader
                tabla.Load(resul); // cargamos los datos del datareader a la datatable
                return tabla; //retornamos los datos 
            }
            catch (Exception ex)
            {
                return null;
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

        public string Insertar(Ingreso obj)
        {
            string rpta = "";
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("sp_ingreso_insertar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idproveedor", SqlDbType.Int).Value = obj.IdProveedor;
                cmd.Parameters.Add("@idusuario", SqlDbType.Int).Value = obj.IdUsuario;
                cmd.Parameters.Add("@tipo_comprobante", SqlDbType.VarChar).Value = obj.TipoComprobante;
                cmd.Parameters.Add("@serie_comprobante", SqlDbType.VarChar).Value = obj.SerieComprobante;
                cmd.Parameters.Add("@num_comprobante", SqlDbType.VarChar).Value = obj.NumComprobante;
                cmd.Parameters.Add("@impuesto", SqlDbType.Decimal).Value = obj.impuesto;
                cmd.Parameters.Add("@total", SqlDbType.Decimal).Value = obj.Total;
                cmd.Parameters.Add("@detalle", SqlDbType.Structured).Value = obj.detalles;
                cn.Open();
                cmd.ExecuteNonQuery();
                rpta ="OK";

            }
            catch (Exception ex)
            {

                rpta = ex.Message;
            }
            finally
            {
                if (cn.State == ConnectionState.Open) //si la conexion esta abierta
                {
                    cn.Close();  // se cierra la conexion
                }
            }
            return rpta;
        }

        public string Anular(int id)
        {
            string rpta = "";
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("sp_ingreso_anular", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idingreso", SqlDbType.Int).Value = id;
                cn.Open();
                cmd.ExecuteNonQuery();
                rpta = "OK";

            }
            catch (Exception ex)
            {

                rpta = ex.Message;
            }
            finally
            {
                if (cn.State == ConnectionState.Open) //si la conexion esta abierta
                {
                    cn.Close();  // se cierra la conexion
                }
            }
            return rpta;
        }
    }
}
