using System;
using System.Data;
using System.Data.OleDb;

namespace GasMileageWPF_MVVM.DataAccess
{
    class DAO
    {
        // MS Access
        private static OleDbConnection OLEconnection = null;
        private static string strConnection;

        private static string provider = "Provider=Microsoft.Jet.OLEDB.4.0;";
        private static string datasource = @"Data Source=C:\Users\mark\Documents\Gas Mileage\Access Database\";
        //private static string datasource = "Data Source=C:\\Users\\mark\\Documents\\Gas Mileage\\Access Database\\";
        private static string database = "Mileage.mdb";

        // SQLite
        //private static string SQLiteCnxnString = @"Data Source=C:\Users\mark\Documents\Gas Mileage\SQLite Database\GasMileage.sqlite;Version=3;";
        private static string SQLiteCnxnString = @"Data Source=G:\Documents\Gas Mileage\SQLite Database\GasMileage.sqlite;Version=3;";
        
        //--------------------------------------------------------------------|
        public static OleDbConnection getOLEDBconnection()
        {
            Console.WriteLine ( "Getting OLE DB Connection..." );
            string connection = getDBconnectionString();
            OLEconnection = new OleDbConnection(connection);

            return OLEconnection;
        }
        //--------------------------------------------------------------------|
        public static string getDBconnectionString()
        {
            Console.WriteLine ( "Getting DB Connection string..." );
            return strConnection = provider + datasource + database;
        }
        //--------------------------------------------------------------------|
        public static string getSQLiteConnectionString()
        {
            //return SQLiteCnxnString;
            //return SQLiteUSBCnxnString;
            return SQLiteCnxnString;
        }
        //--------------------------------------------------------------------|
        public static string getDistinctAutomobileQuery()
        {
            return "SELECT DISTINCT Automobile " +
                   "FROM            GasMileage " +
                   "ORDER BY        Automobile DESC";
        }
        //--------------------------------------------------------------------|
        public static string getDistinctYears()
        {
            return "SELECT DISTINCT Year " +
                   "FROM            GasMileage " +
                   "ORDER BY        Year DESC";
        }
        //--------------------------------------------------------------------|
        public static string getDistinctMonths()
        {
            return "SELECT DISTINCT Month " +
                   "FROM            GasMileage " +
                   "ORDER BY        Month";
        }
        //--------------------------------------------------------------------|
        public static string getDistinctDays()
        {
            return "SELECT DISTINCT Day " +
                   "FROM            GasMileage " +
                   "ORDER BY        Day";
        }
        //--------------------------------------------------------------------|
        public static string getCurrentDay()
        {
            DateTime today = DateTime.Today;

            return "SELECT DISTINCT Day " +
                   "FROM GasMileage " +
                   "WHERE Day = " + today.Day;
        }
        //--------------------------------------------------------------------|
        public static string getMaxOdometer()
        {
            return "SELECT MAX(Odometer) " +
                   "FROM GasMileage ";
        }
        //--------------------------------------------------------------------|
        public static string getGasMileageData()
        {
            return "SELECT  Automobile, Year, Month, Day, Odometer, Trip, ROUND(Gallons, 3) AS Gals, ROUND(Cost, 2) AS Cost, ROUND((Trip/Gallons), 2) AS MPG " +
                   "FROM GasMileage " +
                   "ORDER BY Automobile DESC, Year DESC, Month DESC, Day DESC";
        }

    }
}
