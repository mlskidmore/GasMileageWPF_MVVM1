using System;

namespace GasMileageWPF_MVVM.Model
{
    class Day
    {
        public float fltDay { get; set; }
        public string strDay { get; set; }

        public Day(float day)
        {
            this.fltDay = day;
            strDay = day.ToString();
        }
    }
}
