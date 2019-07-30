using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnectionTest
{
    public class DBRead
    {
        public static string itemList()
        {
            string sql = "select * from testTable";
            string s = "<br />";
            try
            {
                OracleCommand cmd = new OracleCommand(sql, DBConnect.con);
                OracleDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    s = s
                        + r["item_id"].ToString() + ", "
                        + r["item_name"].ToString() + ", "
                        + "£" + r["price_consumer"].ToString()
                        + "<br />";
                }
            }
            catch (Exception e)
            {
                s =(e.ToString());
            }
            return s;
        }
    }
}
