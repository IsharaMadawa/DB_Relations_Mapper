using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Collections;

namespace DB_Connector
{
    public class PostgrSqlDbConnector
    {
        //get data from config file to build connection string
        String userId = System.Configuration.ConfigurationManager.AppSettings["postgeruserId"];
        String pass = System.Configuration.ConfigurationManager.AppSettings["postgerPassword"];
        String host = System.Configuration.ConfigurationManager.AppSettings["postgerHost"];
        String port = System.Configuration.ConfigurationManager.AppSettings["postgerPort"];
        String database = System.Configuration.ConfigurationManager.AppSettings["postgerDatabase"];
        NpgsqlConnection con;

        public NpgsqlConnection GetCon()
        {
            //build connection string 
            string conString = "User ID="+userId+";Password="+pass+";Host="+host+";Port="+port+";Database="+database;
            con = new NpgsqlConnection(conString);
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
            catch (NpgsqlException e)
            {
                status["Connection :"] = "Error";
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
