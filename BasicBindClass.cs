using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace zmvvm1
{
    /// <summary>
    /// 
    /// </summary>
    public delegate bool validaDel<T>(T value, ref string sError);

    public class BasicBindClass<T> : ViewModelBindBaseClass
    {
        //public NotifyDataErrorInfoClass notifyDataErrorInfo;
        public BasicBindClass()
        {
           
        }
        public BasicBindClass(T valor)
        {
            _data = valor;
        }
        public BasicBindClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo, validaDel<T> valida)
        {
            // this.basicStringBind();
            this.notifyDataErrorInfo = notifyDataErrorInfo;
           
            this.valida = valida;
        }
        public BasicBindClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo, validaDel<T> valida,T valor)
        {
            // this.basicStringBind();
            this.notifyDataErrorInfo = notifyDataErrorInfo;

            this.valida = valida;
            _data = valor;
        }

        public BasicBindClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo, validaDel<T> valida, bool esObligatorio)
        {
            // this.basicStringBind();
            this.notifyDataErrorInfo = notifyDataErrorInfo;
           
            this.valida = valida;
            this.esObligatorio = esObligatorio;
        }
        public BasicBindClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo, validaDel<T> valida, bool esObligatorio, T valor)
        {
            // this.basicStringBind();
            this.notifyDataErrorInfo = notifyDataErrorInfo;

            this.valida = valida;
            this.esObligatorio = esObligatorio;
            _data = valor;
        }
        string nameProperty = "b_data";
        String sError;
        bool esObligatorio = false;

        private validaDel<T> valida = null;
        private T _data { get; set; }
        private T _b_data { get; set; }

        // binded propertye
        public T b_data
        {
            get { if ( notifyDataErrorInfo!=null && notifyDataErrorInfo.HasErrorProperty(nameProperty) == true) return (_b_data); else  return (_data); }
            set
            {
               // if (valida != null)
                {

                    T valor = value;
                    if (esObligatorio == true)
                    {
                        if (valor == null 
                            || (typeof(T)==typeof(String) && valor.ToString() == "")
                            || (typeof(T) == typeof(string) && valor.ToString() == "")
                            )
                        {
                            _b_data = valor;
                            RaiseErrorsChanged(nameProperty, "Dato obligatorio");
                            return;
                        }
                    }

                    if (valida==null || valida(valor, ref sError) == true)
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

        public T data
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