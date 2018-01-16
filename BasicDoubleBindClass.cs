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
    public delegate bool validaDoubleDel(Double value, ref string sError);
    public class BasicDoubleBindClass : ViewModelBindBaseClass
    {
        //public NotifyDataErrorInfoClass notifyDataErrorInfo;
        public BasicDoubleBindClass()
        {
            initBasicStringBindClass();
        }
        public BasicDoubleBindClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo, validaDoubleDel valida)
        {
            // this.basicStringBind();
            this.notifyDataErrorInfo = notifyDataErrorInfo;
            initBasicStringBindClass();
            this.valida = valida;
        }
        public BasicDoubleBindClass(NotifyDataErrorInfoDictionaryClass notifyDataErrorInfo, validaDoubleDel valida, bool esObligatorio)
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
        CommandClass comando;
        private validaDoubleDel valida = null;
        private double? _data { get; set; }
        private String _b_data { get; set; }

        // binded propertye
        public String b_data
        {
            get { if (notifyDataErrorInfo.HasErrorProperty(nameProperty) == true) return (_b_data); else  return (_data.ToString()); }
            set
            {
                if (valida != null)
                {

                    string valor = value;
                    if (esObligatorio == true)
                    {
                        if (valor == null || valor == "")
                        {
                            _b_data = valor;
                            RaiseErrorsChanged(nameProperty, "Dato obligatorio");
                            return;
                        }
                    }
                    double valorDouble;
                    if (Double.TryParse(valor, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.CurrentCulture, out valorDouble)==false)
                    {
                        _b_data = valor;
                        RaiseErrorsChanged(nameProperty, "No es un número");
                        return;
                    }
                    if (valida(valorDouble, ref sError) == true)
                    {

                        _data = valorDouble;
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

        public Double? data
        {
            get { return (_data); }
            set
            {
                b_data = value.ToString();
                NotifyOfPropertyChanged(nameProperty);
            }
        }
    }
}
