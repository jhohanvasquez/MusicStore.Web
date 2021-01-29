using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Data.Common
{
    public class ConnectionSql
    {
        #region Metodos
        /*defino una region para los metodos*/


        public SqlConnection Conectar(string pclave)
        {
            SqlConnection Conecction = new SqlConnection(ConfigurationSettings.AppSettings[pclave].ToString());
            Conecction.Open();
            return Conecction;
            /*con este me conecto y abro la base de datos*/
        }
        public bool Actualizar(SqlConnection pconexion, string pprocedimiento, SqlParameter[] pparametro)
        {
            SqlCommand oCmd = new SqlCommand();
            try
            {

                oCmd.CommandType = CommandType.StoredProcedure;/*tipo de comando co procedimiento*/
                oCmd.CommandText = pprocedimiento;
                oCmd.Connection = pconexion;
                foreach (var parametro in pparametro)/*desde el primer parametro hasta el ultimo parametro*/
                {
                    oCmd.Parameters.Add(parametro).Value = parametro.Value;
                }
                oCmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                oCmd.Connection.Close();
                pconexion.Close();
            }


            /*con este Metodo puedo realizar cualquier coneccion a una base de datos y enviar parametro a cualquier procedimiento almacenado*/
        }

        public DataSet Consulta(SqlConnection pconexion, string pprocedimiento, SqlParameter[] pparametro)
        {
            SqlCommand oCmd = new SqlCommand();
            try
            {

                oCmd.CommandType = CommandType.StoredProcedure;/*tipo de comando co procedimiento*/
                oCmd.CommandText = pprocedimiento;
                oCmd.Connection = pconexion;
                foreach (var parametro in pparametro)/*desde el primer parametro hasta el ultimo parametro*/
                {
                    oCmd.Parameters.Add(parametro).Value = parametro.Value;
                }
                SqlDataAdapter odato = new SqlDataAdapter(oCmd);
                DataSet odataset = new DataSet();
                odato.Fill(odataset);
                return odataset;
            }
            catch (SqlException ex)
            { throw new Exception(ex.Message); }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                oCmd.Connection.Close();
                pconexion.Close();
            }
            /*con este Metodo puedo realizar cualquier coneccion a una base de datos y enviar parametro a cualquier procedimiento almacenado para consultar*/
        }


        public DataSet Consulta(SqlConnection pconexion, string pprocedimiento)
        /*Sobrecargar del Metodo*/
        {
            SqlCommand oCmd = new SqlCommand();
            try
            {

                oCmd.CommandType = CommandType.StoredProcedure;/*tipo de comando con procedimiento*/
                oCmd.CommandText = pprocedimiento;
                oCmd.Connection = pconexion;
                SqlDataAdapter odato = new SqlDataAdapter(oCmd);
                DataSet odataset = new DataSet();
                odato.Fill(odataset);
                return odataset;
            }
            catch (SqlException ex)
            { throw new Exception(ex.Message); }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                oCmd.Connection.Close();
                pconexion.Close();
            }
            /*con este Metodo puedo realizar cualquier coneccion a una base de datos y enviar parametro a cualquier procedimiento almacenado para consultar*/
        }



    }
    #endregion
}

