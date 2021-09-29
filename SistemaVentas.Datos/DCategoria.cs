﻿using SistemaVentas.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Datos
{
   public class DCategoria
    {
        public DataTable Listar()
        {
            SqlDataReader resul;
            DataTable tabla = new DataTable();
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("sp_listar_categoria",cn);//PROCEDURES
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
                SqlCommand cmd = new SqlCommand("sp_buscar_categoria", cn);//PROCEDURES
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
        public DataTable Seleccionar()
        {
            SqlDataReader resul;
            DataTable tabla = new DataTable();
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("sp_categoria_seleccionar", cn);//PROCEDURES
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

        public string Existencia(string valor)
        {
            string rpta = "";
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("sp_categoria_existe", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;
                SqlParameter existe = new SqlParameter();
                existe.ParameterName = "@existe"; //seleecionamos el parametro
                existe.SqlDbType = SqlDbType.Int; // dato entero 1 o 0
                existe.Direction = ParameterDirection.Output; //parametro de salida
                cmd.Parameters.Add(existe); 
              
                cn.Open();
                cmd.ExecuteNonQuery();
                rpta = Convert.ToString(existe.Value);
               

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
        public string Insertar(Categoria obj)
        {
            string rpta = "";
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("sp_insertar_categoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = obj.Nombre;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = obj.Descripcion;
                cn.Open();
                rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo ingresar el registro";
                              
            }
            catch (Exception ex)
            {

                rpta= ex.Message;
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

        public string Actualizar(Categoria obj)
        {
            string rpta = "";
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("sp_actualizar_categoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idcategoria", SqlDbType.Int).Value = obj.IdCategoria;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = obj.Nombre;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = obj.Descripcion;
                cn.Open();

                rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo actualizar el registro";

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

        public string Eliminar(int id)
        {
            string rpta = "";
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("sp_eliminar_categoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;
                cn.Open();

                rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo eliminar el registro";

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

        public string Activar(int id)
        {
            string rpta = "";
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("sp_activar_categoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;
                cn.Open();

                rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo activar el registro";

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
        public string Desactivar(int id)
        {
            string rpta = "";
            SqlConnection cn = new SqlConnection();
            try
            {
                cn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("sp_desactivar_categoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;
                cn.Open();

                rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se pudo desactivar el registro";

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
