using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace zmvvm1
{
   public class NotifyDataErrorInfoDictionaryClass 
    {
        public Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public bool HasErrors()
        {
            // _errors es el diccionario
            return _errors.Values.FirstOrDefault(l => l.Count > 0) != null; 
        }
        public bool HasErrorProperty(string propertyName)
        {
            //List<string> errorsForName=GetErrors(propertyName).toList
            //System.Collections.IEnumerable errors = GetErrors(propertyName);
            List<string> errorsForProperty;
            if (_errors.TryGetValue(propertyName, out errorsForProperty) == false)
                return (false);
            if (errorsForProperty.Count == 0)
                return (false);
            return (true);
            /* antiguo sistema, no se si funcionaba bien 
             List<string> lerrors;
             try
             {
                 lerrors = GetErrors(propertyName).Cast<string>().ToList();
             }
             catch { return false; }

            if (lerrors.Count > 0)
                return (true);
            return (false);
             */
        }
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            List<string> errorsForName;
            _errors.TryGetValue(propertyName, out errorsForName);
            return errorsForName;
        }

       
        public bool CanRaiseErrorsChanged(string propertyName, string sError)
        {
            List<string> errorsForProperty;
            if (_errors.TryGetValue(propertyName, out errorsForProperty) == false)
                errorsForProperty = new List<string>();
            int erroresCount = errorsForProperty.Count;



            //Check si existe lista de errores de nuestro propertyName  


            // No hay errores grabados y hay que limpiar errores  
            if (erroresCount == 0 && String.IsNullOrEmpty(sError)) return(false);// No notifico nada
            // Hay errores grabados y queremos registrar un error
            //if (erroresCount>0 && !String.IsNullOrEmpty(sError)) return;// No notifico



            errorsForProperty.Clear();

            if (!String.IsNullOrEmpty(sError))
                errorsForProperty.Add(sError);
            _errors[propertyName] = errorsForProperty;
            return (true);
        }

    }
}
