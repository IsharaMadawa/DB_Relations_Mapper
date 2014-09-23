using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using DB_Connector;

namespace DBDataReaders
{
    public class MongoDBReader
    {
        public void read()
        {
            Console.WriteLine("Connecting....");
            string dbname = System.Configuration.ConfigurationManager.AppSettings["mongodatabase"];
            MongoDBConnector getCon = new MongoDBConnector();
            MongoServer con = getCon.GetCon();
            try
            {
                con.Connect();
                Console.WriteLine("Connected\n");
                var db = con.GetDatabase("est");
                using (con.RequestStart(db))
                {
                    var collection = db.GetCollection<BsonDocument>("user_login");
                    var query = new QueryDocument();
                    foreach (BsonDocument record in collection.Find(query))
                    {
                        BsonElement Id = record.GetElement("id");
                        BsonElement Fname = record.GetElement("fname");
                        BsonElement Lname = record.GetElement("lname");
                        BsonElement Password = record.GetElement("password");
                        Console.WriteLine("{0}\t{1}" + " " + "{2}\t{3}", Id.Value.ToString(), Fname.Value.ToString(), Lname.Value.ToString(), Password.Value.ToString());                       
                    }
                    con.Disconnect();
                }
            }
            catch (MongoException e)
            {
                Console.WriteLine("\tError: " + e.HResult.ToString() + "\n\t has occurred: " + e.Message.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if(con.State == MongoServerState.Connected)
                {
                    con.Disconnect();
                }
            }
        }
    }
}
