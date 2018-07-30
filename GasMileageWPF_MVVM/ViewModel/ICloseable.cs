using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasMileageWPF_MVVM.ViewModel
{
    interface ICloseable
    {
        event EventHandler<EventArgs> RequestClose;
    }
}
