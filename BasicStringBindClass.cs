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
    //public delegate bool validaStringDel(String value, ref string sError);

    public class BasicStringBindClass<T> : ViewModelBindBaseClass
    {
        //public NotifyDataErrorInfoClass notifyDataErrorInfo;
        public BasicStringBindClass()
        {
           
        }

        public BasicStringBindClass(T valor)
        {
            _data = valor;
        }
        public BasicStringBindClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo, validaDel<T> valida,CommandClass comando)
        {
            // this.basicStringBind();
            this.notifyDataErrorInfo = notifyDataErrorInfo;
           
            this.valida = valida;
            this.comando = comando;
        }
        /* Este no puede existir porque es inconsistente con el siguiente si T es bool 
        public BasicBindClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo, validaDel<T> valida,T valor)
        {
            // this.basicStringBind();
            this.notifyDataErrorInfo = notifyDataErrorInfo;

            this.valida = valida;
            _data = valor;
        }
        */
        public BasicStringBindClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo, validaDel<T> valida, bool esObligatorio,CommandClass comando)
        {
            // this.basicStringBind();
            this.notifyDataErrorInfo = notifyDataErrorInfo;
           
            this.valida = valida;
            this.esObligatorio = esObligatorio;
            this.comando = comando;
        }
        public BasicStringBindClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo, validaDel<T> valida, bool esObligatorio, T valor, CommandClass comando)
        {
            // this.basicStringBind();
            this.notifyDataErrorInfo = notifyDataErrorInfo;

            this.valida = valida;
            this.esObligatorio = esObligatorio;
            this.comando = comando;
            _data = valor;
        }
        public void clearErrors()
        {
            ClearErrors(nameProperty);
        }

        string nameProperty = "b_data";
        string namePropertyFormat = "f_data";
        String sError;
        bool esObligatorio = false;
        CommandClass comando=null;
        private validaDel<T> valida = null;
        private T _data { get; set; }
        private String _b_data { get; set; }
        public IzConvert<T> convert;

        public String f_data
        {
            get
            {
                if (notifyDataErrorInfo != null && notifyDataErrorInfo.HasErrorProperty(nameProperty) == true) return (_b_data);
                else
                {
                    return convert.formatear(data, b_data);
                    return (b_data);
                }
            }

        }

        // binded propertye

        public String b_data
        {
            get
            {
                if (notifyDataErrorInfo != null && notifyDataErrorInfo.HasErrorProperty(nameProperty) == true) return (_b_data);
                else
                {
                    string x=convert.convertToString(_data);
                    return x;
                }
            }
            set
            {
                String sError="";


               // if (valida != null)
                {
                    String valor = value;
                    if (convert.valida(valor,ref sError)==false)
                    {
                        _b_data = valor;
                        RaiseErrorsChanged(nameProperty, sError);
                        if (comando != null)
                            comando.notifyChange();
                        NotifyOfPropertyChanged(namePropertyFormat);
                        return;

                    }

                    
                    if (esObligatorio == true)
                    {
                        if (valor == null 
                            || valor == "")
                            
                            
                        {
                            _b_data = valor;
                            RaiseErrorsChanged(nameProperty, "Dato obligatorio");
                            if (comando != null) 
                                comando.notifyChange();
                            NotifyOfPropertyChanged(namePropertyFormat);
                            return;
                        }
                    }
                    T tValor = convert.convert(valor);
                    if (valida==null || valida(tValor, ref sError) == true)
                    {

                        _data = tValor;
                        ClearErrors(nameProperty);
                        if (comando != null)
                            comando.notifyChange();
                        // Cuidado con valores recursivos
                    }
                    else
                    {
                        _b_data = valor;
                        RaiseErrorsChanged(nameProperty, sError);

                        if (comando != null) comando.notifyChange();
                        // Cuidado con valores recursivos
                    }
                    NotifyOfPropertyChanged(namePropertyFormat);

                }
            }
        }

        public T data
        {
            get { return (_data); }
            set
            {
                b_data = convert.convertToString(value);
                NotifyOfPropertyChanged(nameProperty);
                NotifyOfPropertyChanged(namePropertyFormat);

            }
        }
    }
}