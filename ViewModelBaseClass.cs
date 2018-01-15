using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace zmvvm1
{
   public class ViewModelBaseClass : INotifyPropertyChanged
    {
       public NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo = new NotifyDataErrorInfoDictionaryClass();
       
       public event PropertyChangedEventHandler PropertyChanged;
       public void NotifyOfPropertyChanged(string prop)
       {
           if (PropertyChanged != null)
           {
               PropertyChanged(this, new PropertyChangedEventArgs(prop));
           }
       }
    }
}
