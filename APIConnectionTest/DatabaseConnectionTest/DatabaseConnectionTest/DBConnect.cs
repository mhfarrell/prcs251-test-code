using Oracle.ManagedDataAccess.Client;

namespace DatabaseConnectionTest
{
    public class DBConnect
    {
        public static OracleConnection con;
        public static void connectDB()
        {
            con = new OracleConnection();
            con.ConnectionString = "Data Source=(DESCRIPTION=" //between here
                                                  + "(ADDRESS="
                                                      + " (PROTOCOL=TCP)"
                                                      + " (HOST=larry.uopnet.plymouth.ac.uk)"
                                                      + " (PORT=1521))"
                                                  + " (CONNECT_DATA="
                                                      + " (SID=ORCL)));" //And here works
                                                  + "User Id=PRCS251I;" // (same as your email without the @ and any punctuation)
                                                  + "Password=PIZZAPLANET251I;";//password should be all uppercase i think by default it was cap A followed by your student number. also fyi my password is not password.
            con.Open();
            return;
        }

        public static void closeDB()
        {
            con.Close();
            con.Dispose();
            return;
        }

        public static string serverVersion()
        {
            string r = "Connected to Oracle: " + (con.ServerVersion).ToString() + "<br />";
            return r;
        }
    }
}
