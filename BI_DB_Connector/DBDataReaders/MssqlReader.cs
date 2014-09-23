using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DB_Connector;
using System.Collections;
using System.Data;
using System.Configuration;

namespace DBDataReaders
{
    public class MssqlReader
    {
        public void Read(string tabalName)
        {
            Console.WriteLine("Connecting.....");
            MsSqlDBConnector getCon = new MsSqlDBConnector();
            SqlConnection con = getCon.GetCon();
            try
            {
                con.Open();
                Console.WriteLine("Connected\n");
                string sql = "select * FROM "+tabalName;
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader red = cmd.ExecuteReader();
                DataSet set = new DataSet();
                while (red.Read())
                {
                    for (int i = 0; i < red.FieldCount; i++)
                    {
                        Console.WriteLine(red[i].ToString());
                    }
                    Console.WriteLine("\n----------------------------------\n");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("\tError: " + e.Number.ToString() + "\n\t has occurred: " + e.Message.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        public List<string> ReadRelations(string tbName)
        {
            MsSqlDBConnector dbCon = new MsSqlDBConnector();
            SqlConnection con = dbCon.GetCon();
            try
            {
                con.Open();
                string sql = "EXEC sp_fkeys '" + tbName + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                List<string> joinTables = new List<string>();
                SqlDataReader red = cmd.ExecuteReader();
                joinTables.Add(tbName);
                while (red.Read())
                {
                    joinTables.Add(red["FKTABLE_NAME"].ToString());
                }
                return joinTables;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<string> GetTables()
        { 
            MsSqlDBConnector dbCon = new MsSqlDBConnector();
            SqlConnection con = dbCon.GetCon();
            try
            {
                con.Open();
                string sql = "USE " + ConfigurationManager.AppSettings["initialCatalog"] + " SELECT * FROM INFORMATION_SCHEMA.TABLES";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader red = cmd.ExecuteReader();
                List<string> tableNames = new List<string>();
                while (red.Read())
                {
                    tableNames.Add(red["TABLE_NAME"].ToString());
                }
                return tableNames;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
