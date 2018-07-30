using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasMileageWPF_MVVM.Model
{
    public class FillUp
    {
        private string _automobile;
        public string Automobile
        {
            get { return _automobile; }
            set { _automobile = value; }
        }

        private int _year;
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        private float _month;
        public float Month
        {
            get { return _month; }
            set { _month = value; }
        }

        private float _day;
        public float Day
        {
            get { return _day; }
            set { _day = value; }
        }

        private float _odometer;
        public float Odometer
        {
            get { return _odometer; }
            set { _odometer = value; }
        }

        private float _tripOdometer;
        public float TripOdometer
        {
            get { return _tripOdometer; }
            set { _tripOdometer = value; }
        }

        private float _gallons;
        public float Gallons
        {
            get { return _gallons; }
            set { _gallons = value; }
        }

        private float _cost;
        public float Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

        /*public static void Save(Fillup fillup)
        {
            Console.WriteLine("Odometer: " + fillup._odometer);
        }*/

    }
}
