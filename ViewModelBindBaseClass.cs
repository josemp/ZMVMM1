using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace zmvvm1
{



    /// <summary>
    /// Example 
    ///         private string _name;
    ///    public string name
    ///    {
    ///        get { return _name; }
    ///        set
    ///        {
    ///            if (_name == value) return;
    ///            _name = value;
    ///            if (name=="pepe")
    ///                RaiseErrorsChanged("name","Este nombre no vale");
    ///            else
    ///                RaiseErrorsChanged("name", null);
    ///            OnPropertyChanged("name");
    ///        }
    ///    }
    /// </summary>
   public abstract class ViewModelBindBaseClass : INotifyPropertyChanged , INotifyDataErrorInfo
    {

       public NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo;
       public bool HasErrors
       {
           // _errors es el diccionario
           get { if (notifyDataErrorInfo != null) return notifyDataErrorInfo.HasErrors(); return false; }
       }
       public System.Collections.IEnumerable GetErrors(string propertyName)
       {
           if (notifyDataErrorInfo == null) return (null);
           return notifyDataErrorInfo.GetErrors(propertyName);
       }

       public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
       public void RaiseErrorsChanged(string propertyName, string sError)
       {
           if (notifyDataErrorInfo == null) return ;
           if (notifyDataErrorInfo.CanRaiseErrorsChanged(propertyName, sError) == true)
           {
               EventHandler<DataErrorsChangedEventArgs> handler = ErrorsChanged;
               if (handler == null) return;
               var arg = new DataErrorsChangedEventArgs(propertyName);
               handler.Invoke(this, arg);
           }
       }
       public void ClearErrors(string propertyName)
       {
           RaiseErrorsChanged(propertyName, null);
       }
 
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
