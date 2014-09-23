using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB_Connector;
using MySql.Data.MySqlClient;

namespace DBDataReaders
{
    public class MysqlReader
    {
        public void Read()
        {
            Console.WriteLine("Connecting.........");
            MySqlDBConnector getCon = new MySqlDBConnector();
            MySqlConnection con = getCon.GetCon();
            try
            {
                con.Open();
                Console.WriteLine("Connected\n");
                string sql = "select user_id,fname,lname,password from user_login";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader red = cmd.ExecuteReader();
                while (red.Read())
                {
                    Console.WriteLine("{0}\t{1}" + " " + "{2}\t{3}", red["user_id"].ToString(), red["Fname"].ToString(), red["Lname"].ToString(), red["Password"].ToString());
                }
            }
            catch (MySqlException e)
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
                    con.Clone();
                }
            }


        }

    }
}
