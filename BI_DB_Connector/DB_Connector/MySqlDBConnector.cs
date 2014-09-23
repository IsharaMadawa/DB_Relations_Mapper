using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections;

namespace DB_Connector
{
    public class MySqlDBConnector
    {
        //get data from config file to build connection string
        String server = System.Configuration.ConfigurationManager.AppSettings["mysqlServer"];
        String db = System.Configuration.ConfigurationManager.AppSettings["mysqlDatabase"];
        String userid = System.Configuration.ConfigurationManager.AppSettings["mysqluserId"];
        String pass = System.Configuration.ConfigurationManager.AppSettings["mysqlpassword"];
        MySqlConnection con;

        public MySqlConnection GetCon()
        {
            //build connection string
            String conString = "Server="+server+";Database="+db+";Uid="+userid+";Pwd="+pass;
            con = new MySqlConnection(conString);
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
            catch (MySqlException e)
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
