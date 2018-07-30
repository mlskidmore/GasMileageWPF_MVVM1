using System;
using System.ComponentModel;

namespace GasMileageWPF_MVVM.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        protected ViewModelBase()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /*protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                handler(this, args);
            }
        }*/

        public void RaisePropertyChanged(string propertyName)
        {
            Console.WriteLine("Executing RaisePropertyChanged() for " + propertyName);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Dispose()
        {
            this.OnDispose();
        }

        protected virtual void OnDispose()
        {
        }
    }
}
