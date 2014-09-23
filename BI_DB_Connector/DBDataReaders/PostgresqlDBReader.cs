using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB_Connector;
using Npgsql;

namespace DBDataReaders
{
    public class PostgresqlDBReader
    {
        public void Read()
        {
            Console.WriteLine("Connecting....");
            PostgrSqlDbConnector getCon = new PostgrSqlDbConnector();
            NpgsqlConnection con = getCon.GetCon();
            try
            {
                con.Open();
                Console.WriteLine("Connected\n");

                string query = "selec * from user_login.user_login";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                NpgsqlDataReader red = cmd.ExecuteReader();
                while (red.Read())
                {
                    Console.WriteLine("{0}\t{1}" + " " + "{2}\t{3}", red["user_id"].ToString(), red["fname"].ToString(), red["lname"].ToString(), red["Password"].ToString());
                }
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine("\n\t" + e.Message.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
