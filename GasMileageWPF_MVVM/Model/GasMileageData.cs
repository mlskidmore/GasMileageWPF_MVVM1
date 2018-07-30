using System;
using System.Data;
using System.Data.SQLite;
using System.Windows;

using GasMileageWPF_MVVM.DataAccess;

namespace GasMileageWPF_MVVM.Model
{
    class GasMileageData
    {
        //Form-level declared connection using user-defined DBconnection class
        string SQLiteCnxnString = DAO.getSQLiteConnectionString();

        //---------------------------------------------------------------------
        public GasMileageData()
        {
            Console.WriteLine("\nInstantiating GasMileageData, getting TableData ...\n");
            TableData = new DataTable();
        }
        //---------------------------------------------------------------------
        public DataTable TableData
        {
            get;
            set;
        }
        
        //---------------------------------------------------------------------
        public bool getGasMileageData()
        {
            SQLiteConnection sqliteConn = new SQLiteConnection(SQLiteCnxnString);
            DateTime today = DateTime.Today;

            try
            {
                Console.WriteLine("\nHitting SQLite ...\n");
                sqliteConn.Open();

                string SQLquery = DAO.getGasMileageData();
                SQLiteCommand cmd = new SQLiteCommand(SQLquery, sqliteConn);
                cmd.ExecuteNonQuery();

                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(cmd);
                TableData = new DataTable();
                dataAdapter.Fill(TableData);

                sqliteConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        //---------------------------------------------------------------------
    }
}
