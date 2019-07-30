using Oracle.DataAccess;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DatabaseConnectionTest
{
    class Program
    {
        static OracleConnection con;


        static void Main(string[] args)
        {
            Console.WriteLine(initialConnect() + "\n\n");            
            string executeQuery = "";

            Console.Write("Drop existing table? \nPlease select (Y/N): \n");
            executeQuery = Console.ReadLine().ToUpper();
            Console.WriteLine("\n");
            if (executeQuery == "Y")
            {
                tableCheck();
            }

            executeQuery = executeQuery = "";
            Console.Write("Would you like to build the item table? \nPlease select (Y/N): \n");
            executeQuery = Console.ReadLine().ToUpper();
            Console.WriteLine("\n");
            if (executeQuery == "Y")
            {
                buildTable();
            }

            executeQuery = executeQuery = "";
            Console.Write("Would you like to populate the item table? \nPlease select (Y/N): \n");
            executeQuery = Console.ReadLine().ToUpper();
            Console.WriteLine("\n");
            if (executeQuery == "Y")
            {
                populatedTable();
            }

            executeQuery = executeQuery = "";
            Console.Write("Would you like to display the contents of item table? \nPlease select (Y/N): \n");
            executeQuery = Console.ReadLine().ToUpper();
            Console.WriteLine("\n");
            if (executeQuery == "Y")
            {                
                itemList();
            }

            Console.WriteLine("Press Any Key To Exit");
            Console.ReadKey();
        }

        static string initialConnect()
        {
            Connect();
            string r = "Connected to Oracle: " + (con.ServerVersion).ToString();
            Close();
            return r;
        }


        static void tableCheck()
        {
            try
            {
                string sqlBuild = "drop table testTable"; //"select * from user_objects where object_name = 'item'";
                Connect();
                OracleCommand cmd = new OracleCommand(sqlBuild, con);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                Console.WriteLine("Test Table dropped.");
                Close();
            }
            catch (Exception)
            {
                Console.WriteLine("No Tables Called testTable Exist.");
            }
            Console.WriteLine("\n");
        }
        static void buildTable()
        {
            try
            {
                string sqlBuild = "create table testTable("
                                                        +"pizza_id varchar2(2),"
                                                        +"pizza_name varchar2(16),"
                                                        +"pizza_price number(8))";
                Connect();
                OracleCommand cmd = new OracleCommand(sqlBuild, con);                
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table Created \n\n");
                Close();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\n");
        }

        static void populatedTable()
        {
            try
            {
                string[,] items;
                items = new string[5, 3] { { "1", "Mozzarella", "9.99" },
                                       { "2", "Peperoni", "12.99" },
                                       { "3", "Meat Feast", "14.99" },
                                       { "4", "Chicken Tikka", "12.99" },
                                       { "5", "Spicy Vegetarian", "11.99" } };
                string sqlBuild = "";
                for (int i = 0; i < items.GetLength(0); i++)
                {
                    sqlBuild = "insert into testTable(pizza_id, pizza_name, pizza_price) values("
                                        + "'" + items[i, 0].ToString() + "', "
                                        + "'" + items[i, 1].ToString() + "', "
                                        + "'" + items[i, 2].ToString() + "')";
                    Connect();
                    OracleCommand cmd = new OracleCommand(sqlBuild, con);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Insert " + i + " Complete \n");
                    Close();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\n");
        }

        static void itemList()
        {
            string sql = "select * from Item";
            string s = "\n";
            try
            {
                Connect();
                OracleCommand cmd = new OracleCommand(sql, con);
                OracleDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    s = s 
                        + r["item_id"].ToString() + ", " 
                        + r["item_name"].ToString() + ", " 
                        + "£" +r["price_consumer"].ToString() + "\n";
                }
                Close();
                Console.WriteLine(s);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\n");
        }

        static void Connect()
        {
            con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION=" //between here
                                                  + "(ADDRESS="
                                                      +" (PROTOCOL=TCP)"
                                                      +" (HOST=larry.uopnet.plymouth.ac.uk)"
                                                      +" (PORT=1521))"
                                                  +" (CONNECT_DATA="
                                                      +" (SID=ORCL)));" //And here works
                                                  + "User Id=PRCS251I;" // (same as your email without the @ and any punctuation)
                                                  + "Password=PIZZAPLANET251I;";//password should be all uppercase i think by default it was cap A followed by your student number. also fyi my password is not password.
            con.Open();
        }

        static void Close()
        {
            con.Close();
            con.Dispose();
        }
    }
}
