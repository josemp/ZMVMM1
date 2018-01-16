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
    //public delegate bool validaDel<T>(T value, ref string sError);

    public class BasicBindDClass : ViewModelBindBaseClass
    {
        //public NotifyDataErrorInfoClass notifyDataErrorInfo;
        public BasicBindDClass()
        {
           
        }

        public BasicBindDClass(Double? valor)
        {
            _data = valor;
        }
        public BasicBindDClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo, validaDel<Double?> valida,CommandClass comando)
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
        public BasicBindDClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo, validaDel<Double?> valida, bool esObligatorio,CommandClass comando)
        {
            // this.basicStringBind();
            this.notifyDataErrorInfo = notifyDataErrorInfo;
           
            this.valida = valida;
            this.esObligatorio = esObligatorio;
            this.comando = comando;
        }
        public BasicBindDClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo, validaDel<Double?> valida, bool esObligatorio, Double? valor, CommandClass comando)
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
        String sError;
        bool esObligatorio = false;
        CommandClass comando=null;
        private validaDel<Double?> valida = null;
        private Double? _data { get; set; }
        private string _b_data { get; set; }

        // binded propertye
        public string b_data
        {
            get
            {


                
                    if (notifyDataErrorInfo != null && notifyDataErrorInfo.HasErrorProperty(nameProperty) == true) return (_b_data);
                    else
                    {
                        if (_data == null)
                            return (null);
                        return(_data.ToString());
                    }
            }
            set
            {
               // if (valida != null)
                {

                    string valor = value;
                    Double valorDouble;
                    
                    if (Double.TryParse(valor, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.CurrentCulture, out valorDouble) == false)
                    {
                        _b_data = valor;
                        RaiseErrorsChanged(nameProperty, "Numero Error");
                        if (comando != null)
                            comando.notifyChange();
                        return;

                    }
                    if (esObligatorio == true)
                    {
                        if (valor == null)
                        {
                            _b_data = valor;
                            RaiseErrorsChanged(nameProperty, "Dato obligatorio");
                            if (comando != null) 
                                comando.notifyChange();
                            return;
                        }
                    }

                    if (valida==null || valida(valorDouble, ref sError) == true)
                    {

                        _data = valorDouble;
                        ClearErrors(nameProperty);
                        // Cuidado con valores recursivos
                    }
                    else
                    {
                        _b_data = valor;
                        RaiseErrorsChanged(nameProperty, sError);
                        if (comando != null) comando.notifyChange();
                        // Cuidado con valores recursivos
                    }

                }
            }
        }

        public Double? data
        {
            get { return (_data); }
            set
            {
                b_data = value.ToString();
                NotifyOfPropertyChanged(nameProperty);
            }
        }
        public static T ConvertValue<T>(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}