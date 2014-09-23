using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Data_Readers
{
    class MySqlReader
    {
        public object Reader(object cn)
        {
            string sql = "select * from user_login";
            cn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            MySqlDataReader red = cmd.ExecuteReader();
            return 0;
        }
    }
}
