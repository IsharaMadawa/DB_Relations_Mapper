using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections;

namespace DB_Connector
{
    public class MongoDBConnector
    {
        MongoServer con;
        public MongoServer GetCon()
        {
            string conString = System.Configuration.ConfigurationManager.AppSettings["mongodbconString"];
            con = MongoServer.Create(conString);
            return con;
        }
        public Hashtable CheckDB()
        {
            Hashtable status = new Hashtable();
            GetCon();
            try
            {
                con.Connect();
                status["Connection :"] = "OK";
                return status;
            }
            catch (MongoException e)
            {
                status["Connection :"] = "Error";
                status["Error"] = e.HResult.ToString();
                status["Has occurred :"] = e.Message.ToString();
                return status;
            }
            finally
            {
                con.Disconnect();
            }
            
        }
    }
}
