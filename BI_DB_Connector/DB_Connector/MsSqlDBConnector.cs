using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace DB_Connector
{
    public class MsSqlDBConnector
    {
        //get data from config file
        string dataSource = ConfigurationManager.AppSettings["dataSource"];
        string initialCatalog = ConfigurationManager.AppSettings["initialCatalog"];
        string integratedSecurity = ConfigurationManager.AppSettings["integratedSecurity"];
        SqlConnection con;

        public SqlConnection GetCon()
        {
            //build connection string 
            string conString = "Data Source=" + dataSource + ";Initial Catalog="+initialCatalog+";Integrated Security="+integratedSecurity;
            con = new SqlConnection(conString);
            return con;
        }

        public Hashtable CheckDB()
        {
            Hashtable status = new Hashtable();
            GetCon();
            try
            {
                con.Open();
                status["Connection :"] = "OK";
                return status;
            }
            catch (SqlException e)
            {
                status["Connection :"] = "Error";
                status["Error"] = e.Number.ToString();
                status["Has occurred :"] = e.Message.ToString();
                return status;
            }
            finally
            {
                con.Close();
            }

        }
    }
}
