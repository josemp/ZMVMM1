using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;

namespace zmvvm1
{
    public interface IzConvert<T>
    {
       // Regex regex;
       // String format;
        bool valida(String value);
        bool valida(String value, ref String sError);
        T convert(String value);
        
        String convertToString(T value);
        String formatear(T value,String value1);
    }
    
    public static class zConvert
    {
        public static IzConvert<Double?> doubleBasic = new DoubleBasic();
        public static IzConvert<DateTime?> dateTimeBasic = new DateTimeBasic();
    }

    public class DoubleBasic : IzConvert<Double?>
    {
         
        Regex regex = new Regex("^(([0-9]*)|(([0-9]*)\\.([0-9]*)))$");
        string format = "{0:#0.00}";
        public DoubleBasic(){}

        public DoubleBasic(Regex regex,string format)
        {
            this.regex=regex;
            this.format=format;
        }
        //True if correct
        public bool match(String value)
        {
            if (regex == null)
                return (true);
            try
            {
                return (regex.IsMatch(value));
            }
            catch(Exception e)
            {
                return (false);
            }

        }
        public bool valida(String value, ref String sError)
        {

            Double x;

            if (match(value) == false) { sError = "Caracteres no permitidos"; return (false); }
            bool b = Double.TryParse(value, System.Globalization.NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, System.Globalization.CultureInfo.InvariantCulture, out x);
            if (b == false)
                sError = "No se pudo convertir a número";
            return (b);
        }
        public bool valida(String value)
        {
            string sError = "";
            return valida(value, ref sError);
        }
        public Double? convert(String value)
        {
            Double x;
            if (value == null) return (null);
            bool b = Double.TryParse(value, System.Globalization.NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, System.Globalization.CultureInfo.InvariantCulture, out x);
            Double xx;
           // xx=(Double)Convert.ChangeType((x.ToString(), typeof(Double));
            return x;
        }
        public String convertToString(Double? data)
        {
            if (data == null)
                return (null);
           
            String x = ((Double)data).ToString(CultureInfo.InvariantCulture);
            return x;
        }
       public String formatear(Double? data,String data1)
        {
            if (data==null) return(null);
           if (format==null) return(data1);
           return String.Format(format, data);
        }
        /*
        public static T GetValue<T>(String value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
         * */



    }
    public class DateTimeBasic : IzConvert<DateTime?>
    {

        Regex regex = null;
        public string format = "{0:dd/MM/yyyy}";
        public string editMask = "ddMMyy";
        public string formatEdit()
        {
            return "{0:" + editMask +"}";
        }
        public DateTimeBasic() { }

        public DateTimeBasic(Regex regex, string format)
        {
            this.regex = regex;
            this.format = format;
        }
        //True if correct
        public bool match(String value)
        {
            if (regex == null)
                return (true);
            try
            {
                return (regex.IsMatch(value));
            }
            catch (Exception e)
            {
                return (false);
            }

        }
        public bool valida(String value, ref String sError)
        {

            DateTime x;

            if (match(value) == false) { sError = "Caracteres no permitidos"; return (false); }
            bool b = DateTime.TryParseExact(
                   value,
                   editMask,
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out x);
            if (b == false)
                sError = "No se pudo convertir a número";
            return (b);
        }
        public bool valida(String value)
        {
            string sError = "";
            return valida(value, ref sError);
        }
        public DateTime? convert(String value)
        {
            DateTime x;
            if (value == null) return (null);
            //bool b = DateTime.TryParse(value, "ddMMyy", out x);
            bool b =DateTime.TryParseExact(
                   value,
                   editMask,
                   CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out x);
           return x;
        }
        public String convertToString(DateTime? data)
        {
            if (data == null)
                return (null);
            string f=formatEdit();
            String x = String.Format(formatEdit(), data);
            return x;
            //String x = ((Double)data).ToString(CultureInfo.InvariantCulture);
            //return x;
        }
        public String formatear(DateTime? data, String data1)
        {
            if (data == null) return (null);
            if (format == null) return (data1);
            return String.Format(format, data);
        }
        /*
        public static T GetValue<T>(String value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
         * */



    }
    public class StringBasic : IzConvert<String>
    {

        Regex regex = null;
        String format = null;
        //True if correct
        public bool match(String value)
        {
            if (regex == null)
                return (true);
            try
            {
                return (regex.IsMatch(value));
            }
            catch (Exception e)
            {
                return (false);
            }

        }
        public bool valida(String value, ref String sError)
        {

            
            if (match(value) == false) { sError = "Caracteres no permitidos"; return (false); }
            return (true);
        }
        public bool valida(String value)
        {
            string sError = "";
            return valida(value, ref sError);
        }
        public String convert(String value)
        {
            return (value);
        }
        public String convertToString(String data)
        {
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="data1">b_data, binding data</param>
        /// <returns></returns>
        public String formatear(String data,String data1)
        {
            return (data1);
        }

    }
}

