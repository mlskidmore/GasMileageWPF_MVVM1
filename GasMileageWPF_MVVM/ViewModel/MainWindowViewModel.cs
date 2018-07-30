using GasMileageWPF_MVVM.DataAccess;
using GasMileageWPF_MVVM.Model;
using GasMileageWPF_MVVM.ViewModel.Commands;

using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;

namespace GasMileageWPF_MVVM.ViewModel
{
    class MainWindowViewModel : ViewModelBase, ICloseable
    {
        //Form-level declared connection using user-defined DBconnection class
        string SQLiteCnxnString = DAO.getSQLiteConnectionString();

        public enum ListType { Automobile, Year, Month, Day}

        #region Combobox sources

        public ObservableCollection<Automobile> AutomobilesList { get; set; }
        private Automobile _selectedAuto { get; set; }
        /*
        //---------------------------------------------------------------------        
        public Automobile SelectedAuto
        {
            get
            {
                Console.WriteLine("Get selected auto...");
                return _selectedAuto;
            }
            set
            {
                _selectedAuto = value;
                Console.WriteLine("Set selected auto..." + _selectedAuto.auto.ToString());
                RaisePropertyChanged("SelectedAuto");
            }
        }
        */
        public ObservableCollection<Year> YearsList { get; set; }
        private Year _selectedYear { get; set; }
        /*
        //---------------------------------------------------------------------
        public Year SelectedYear
        {
            get
            {
                Console.WriteLine("Get selected year...");
                return _selectedYear;
            }
            set
            {
                _selectedYear = value;
                Console.WriteLine("Set selected year..." + _selectedYear.year.ToString());
                RaisePropertyChanged("SelectedYear");
            }
        } 
         */
        public ObservableCollection<Month> MonthsList { get; set; }
        //private Month _selectedMonth {get; set; }
        /*
        //---------------------------------------------------------------------
        public Month SelectedMonth
        {
            get
            {
                Console.WriteLine("Get selected month...");
                return _selectedMonth;
            }
            set
            {                
                if (_selectedMonth != value)
                {
                    _selectedMonth = value;
                    Console.WriteLine("Set selected month..." + _selectedMonth.month.ToString());
                    RaisePropertyChanged("SelectedMonth");                    
                }                
            }
        } 
         */
        private int _monthIndex = DateTime.Today.Month - 1;
        //---------------------------------------------------------------------
        public int MonthIndex
        {
            get
            {
                //DateTime today = DateTime.Today;
                //_monthIndex = today.Month - 1;
                Console.WriteLine("Get MonthIndex..." + _monthIndex);
                return _monthIndex;
            }
            set
            {
                if (_monthIndex != value)
                {
                    _monthIndex = value;
                    Console.WriteLine("Set MonthIndex..." + _monthIndex);
                    RaisePropertyChanged("MonthIndex");
                }
            }
        }
        public ObservableCollection<Day> DaysList { get; set; }
        //private Day _selectedDay;
        private int _dayIndex = DateTime.Today.Day - 1;
        //---------------------------------------------------------------------
        public int DayIndex
        {
            get
            {
                Console.WriteLine("Get DayIndex._dayIndex: " + _dayIndex);
                return _dayIndex;
            }

            set
            {
                if (_dayIndex != value)
                {
                    _dayIndex = value;
                    Console.WriteLine("Set current day index..." + _dayIndex);
                    RaisePropertyChanged("DayIndex");
                }
            }
        }
        #endregion       

        private GasMileageData _gasMileageData;        
        private DataTable _tableData;

        private RelayCommand _getDataCommand;
        private RelayCommand _setUpdateButton;

        public event EventHandler<EventArgs> RequestClose;
        
        public ICommand CloseCommand { get; set; }        

        public MainWindowViewModel()
        {
            Console.WriteLine("Instantiating View Model...");

            this.AutomobilesList = new ObservableCollection<Automobile>();
            populateAutomobileComboBox();

            this.YearsList = new ObservableCollection<Year>();
            populateYearComboBox();

            this.MonthsList = new ObservableCollection<Month>();
            populateMonthComboBox();

            this.DaysList = new ObservableCollection<Day>();
            populateDayComboBox();
               
            _gasMileageData = new GasMileageData();
            _tableData = new DataTable();
            //_fillup = new FillUp();            
        }
        //---------------------------------------------------------------------
        /*public FillUp SelectedAuto
        {
            get
            {
                Console.WriteLine("Get selected auto...");
                return _fillup;
            }
            set
            {
                _selectedAuto = value;
                Console.WriteLine("Set selected auto..." + _selectedAuto.auto.ToString());
                RaisePropertyChanged("SelectedAuto");
            }
        }*/
        //---------------------------------------------------------------------
        private void populateAutomobileComboBox()
        {
            Console.WriteLine("\nPopulating Autombiles list...");
            string SQLquery = DAO.getDistinctAutomobileQuery();
            executeDataReader(SQLquery, AutomobilesList, ListType.Automobile);
        }
        private void populateYearComboBox()
        {
            Console.WriteLine("\nPopulating Years list...");
            string SQLquery = DAO.getDistinctYears();
            executeDataReader(SQLquery, YearsList, ListType.Year);
        }           
        //---------------------------------------------------------------------
        private void populateMonthComboBox()
        {
            Console.WriteLine("\nPopulating Months list...");
            string SQLquery = DAO.getDistinctMonths();
            executeDataReader(SQLquery, MonthsList, ListType.Month);            
        }
        //---------------------------------------------------------------------
        public void populateDayComboBox()
        {
            Console.WriteLine("\nPopulating Day list...");
            string SQLquery = DAO.getDistinctDays();
            executeDataReader(SQLquery, DaysList, ListType.Day);
        }
        //---------------------------------------------------------------------
        /*
        public Day SelectedDay
        {
            get
            {
                Console.WriteLine("Get current day...");
                return _selectedDay;
            }
            set
            {
                Console.WriteLine("Set current day...");
                if (_selectedDay != value)
                {
                    _selectedDay = value;
                    RaisePropertyChanged("SelectedDay");
                }
            }
        }
         */
        //---------------------------------------------------------------------
        public void executeDataReader<T>(string SQLquery, ObservableCollection<T> list, ListType listType)
        {
            SQLiteConnection sqliteConn = new SQLiteConnection(SQLiteCnxnString);

            try
            {
                Console.WriteLine("Executing data reader ...");
                sqliteConn.Open();
                SQLiteCommand cmd = new SQLiteCommand(SQLquery, sqliteConn);
                SQLiteDataReader DR = cmd.ExecuteReader();
                int counter = 0;

                Console.WriteLine("DataReader is reading ...");
                while (DR.Read())
                {                    
                    switch (listType)
                    {
                        case ListType.Automobile:
                            string auto = DR.GetString(0);
                            Console.WriteLine("Auto: " + auto);
                            Console.WriteLine ( ++counter + ": Reading Automobiles list..." + auto);
                            this.AutomobilesList.Add(new Automobile(auto));
                            break;

                        case ListType.Year:
                            int year = DR.GetInt32(0);
                            this.YearsList.Add(new Year(year));
                            Console.WriteLine ( ++counter + ": Reading Year list..." + year);
                            break;

                        case ListType.Month:
                            string month = DR.GetString(0);
                            this.MonthsList.Add(new Month(month));
                            Console.WriteLine ( ++counter + ": Reading Month list..." + month);

                            break;
                       
                        case ListType.Day:                            
                            float day = DR.GetFloat(0);
                            Console.WriteLine(++counter + ": Populating Day list..." + day);
                            this.DaysList.Add(new Day(day));
                            break;     
                    }
                }
                sqliteConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //---------------------------------------------------------------------
        public void simpleDataReader(string SQLquery, ref float number)
        {
            SQLiteConnection sqliteConn = new SQLiteConnection(SQLiteCnxnString);
            sqliteConn.Open();
            SQLiteCommand cmd = new SQLiteCommand(SQLquery, sqliteConn);
            SQLiteDataReader DR = cmd.ExecuteReader();
            while (DR.Read())
            {
                number = DR.GetFloat(0);
            }
        }
        //---------------------------------------------------------------------
        public DataTable TableData
        {
            get
            {
                Console.WriteLine("Getting table data...");
                return _tableData;
            }

            set
            {
                _tableData = value;
                RaisePropertyChanged("TableData");
            }
        }
        //---------------------------------------------------------------------
        public ICommand GetDataCommand
        {
            get
            {
                if (_getDataCommand == null)
                {
                    Console.WriteLine("Executing Relay Command...");
                    _getDataCommand = new RelayCommand(param => getData());
                }
                return _getDataCommand;
            }
        }
        //---------------------------------------------------------------------
        public void getData()
        {
            Console.WriteLine("Executing getData() ...");
            _gasMileageData.getGasMileageData();
            TableData = _gasMileageData.TableData;
        }
        //---------------------------------------------------------------------
        public ICommand processUpdateButton
        {
            get
            {
                if (_setUpdateButton == null)
                {
                    Console.WriteLine("Processing Update Button...");
                    _setUpdateButton = new RelayCommand(param => validateOdometer());
                }
                return _setUpdateButton;
            }            
        }
        //---------------------------------------------------------------------
        public void validateOdometer()
        {
            Console.WriteLine("\nValidating odometer...");

            Console.WriteLine("Automobile: " + _selectedAuto.auto);
            Console.WriteLine("Year: " + _selectedYear.year);
            Console.WriteLine("Month Index: " + (_monthIndex));
            Console.WriteLine("Day Index: " + _dayIndex);
        }
    }
}
