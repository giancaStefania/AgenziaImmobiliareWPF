using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmobiliWPF
{
    class DbUtils
    {
        /* To use correctly the connection you have to insert your Data Source after execute scripts in DBScripts folder */
        const string connstr = "Data Source=DESKTOP-EHMDJAU;Initial Catalog=AgenziaImmobiliare;Integrated Security=True";
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connstr);
        }

        public static SqlDataReader GetReader(SqlConnection conn, string sql, Dictionary<string, object> pars)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                if (pars != null && pars.Count > 0)
                {
                    AddParameters(ref cmd, pars);
                }
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine("errore nel metodo GetReader: \n" + ex.Message);
                return null;
            }
        }

        public static int ExecCommand(SqlConnection conn, string sql, Dictionary<string, object> pars)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                if (pars != null && pars.Count > 0)
                {
                    AddParameters(ref cmd, pars);
                }
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("errore nel metodo ExecCommand: \n" + ex.Message);
                return -1;
            }
        }

        public static string LookUP(SqlConnection conn, string sql, Dictionary<string, object> pars)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                if (pars != null && pars.Count > 0)
                {
                    AddParameters(ref cmd, pars);
                }
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return dr[0].ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("errore nel metodo lookUP: \n" + ex.Message);
                return null;
            }
        }

        private static void AddParameters(ref SqlCommand cmd, Dictionary<string, object> pars)
        {
            foreach (KeyValuePair<string, object> pair in pars)
            {
                if(pair.Value != null)
                    cmd.Parameters.Add(pair.Key, GetCorrectDBType(pair.Value.GetType())).Value = pair.Value;
                else
                    cmd.Parameters.Add(pair.Key, SqlDbType.Int).Value = DBNull.Value;
            }
        }

        private static SqlDbType GetCorrectDBType(Type type)
        {
            switch (type.ToString().ToLower())
            {
                case "string":
                    return SqlDbType.VarChar;
                    break;
                case "int":
                    return SqlDbType.Int;
                    break;
                case "double":
                    return SqlDbType.Decimal;
                    break;
                case "long":
                    return SqlDbType.BigInt;
                    break;
                case "bool":
                    return SqlDbType.Bit;
                    break;
                case "datetime":
                    return SqlDbType.DateTime;
                    break;
                default:
                    return SqlDbType.VarChar;
                    break;
            }
        }
    }
}

