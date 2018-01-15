using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zmvvm1
{
    /// <summary>
    /// 
    /// </summary>
    public delegate bool validaStringDel(string value, ref string sError);
      
   public class BasicStringBindClass : ViewModelBindBaseClass 
    {
       //public NotifyDataErrorInfoClass notifyDataErrorInfo;
        public BasicStringBindClass ()
        {
            initBasicStringBindClass();
        }
        public BasicStringBindClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo, validaStringDel valida)
        {
           // this.basicStringBind();
            this.notifyDataErrorInfo = notifyDataErrorInfo;
            initBasicStringBindClass();
            this.valida = valida;
        }
        public BasicStringBindClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo,validaStringDel valida, bool esObligatorio)
        {
            // this.basicStringBind();
            this.notifyDataErrorInfo = notifyDataErrorInfo;
            initBasicStringBindClass();
            this.valida = valida;
            this.esObligatorio = esObligatorio;
        }
        private void initBasicStringBindClass()
        {
            
        }
        string nameProperty = "b_data";
        String sError;
        bool esObligatorio = false;

        private validaStringDel valida= null;
        private String _data { get; set; }
        private String _b_data { get; set; }

        // binded propertye
        public String b_data
        {
            get { if (notifyDataErrorInfo.HasErrorProperty(nameProperty) == true) return (_b_data); else  return (_data); }
            set
            {
                if (valida != null)
                {
                    
                    string valor = value;
                    if (esObligatorio==true)
                    {
                        if (valor==null || valor=="")
                        {
                            _b_data = valor;
                            RaiseErrorsChanged(nameProperty, "Dato obligatorio");
                            return;
                        }
                    }

                    if (valida(valor, ref sError)==true)
                    {

                        _data = valor;
                        ClearErrors(nameProperty);
                        // Cuidado con valores recursivos
                    }
                    else
                    {
                        _b_data = valor;
                        RaiseErrorsChanged(nameProperty, sError);
                        // Cuidado con valores recursivos
                    }

                }
            }
        }

        public String data
        {
            get { return (_data); }
            set
            {
                b_data = value;
                NotifyOfPropertyChanged(nameProperty);
            }
        }
    }
}