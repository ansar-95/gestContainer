using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace tholdi.Ressources
{
    class DataBaseAccess
    {
        public static MySqlConnection getOpenMySqlConnection()
        {
            MySqlConnection msc = new MySqlConnection("Database=mydb_maha;Data Source=217.167.171.227;User Id=maha;Password=06/06/2001;Ssl Mode=None;charset=utf8; convert zero datetime=True");


            msc.Open();
            return msc;
        }
    }
}
