using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shapeless.Core
{
    public class Stringz
    {

        public static int parseInt(string val, int defaultVal)
        {
            int returnVal = defaultVal;
            try
            {
                returnVal = Int32.Parse(val);
            }
            catch (Exception ex)
            {
                returnVal = defaultVal;
            }

            return returnVal;
        }



        public static double parseDouble(string val, double defaultVal)
        {
            double returnVal = defaultVal;
            try
            {
                returnVal = Double.Parse(val);
            }
            catch (Exception ex)
            {
                returnVal = defaultVal;
            }

            return returnVal;
        }

        public static float ParseFloat(string val, float defaultVal)
        {
            float returnVal = defaultVal;
            try
            {
                returnVal = float.Parse(val);
            }
            catch (Exception ex)
            {
                returnVal = defaultVal;
            }

            return returnVal;
        }

        public static DateTime ParseDateTime(string val, DateTime defaultVal)
        {
            DateTime returnVal = defaultVal;
            try
            {
                DateTime.Parse(val);
            }
            catch (Exception ex)
            {
                returnVal = defaultVal;
            }
            return returnVal;
        }


        public static string unionString(char sparChar, params object[] array)
        {
            StringBuilder sbr = new StringBuilder("");
            if (array != null)
            {
                foreach (object obj in array)
                {
                    sbr.Append(obj.ToString()).Append(sparChar);
                }
            }
            return sbr.ToString().Trim(sparChar);
        }

        public static T parse<T>(string value)
        {
            Type type = typeof (T);
            return (T)parse(type, value);
        }

        public static object parse(Type type, string value)
        {
            MethodInfo mth = type.GetMethod("Parse");
            object setValue = mth.Invoke(type,new object[]{value});
            return setValue;
        }

        public static bool isNullOrWhiteSpace(params string[] vals)
        {
            foreach (var val in vals)
            {
                if (string.IsNullOrWhiteSpace(val)) return true;
            }
            return false;
        }

        public static bool isNullOrEmpty(params string[] vals)
        {
            foreach (var val in vals)
            {
                if (string.IsNullOrEmpty(val)) return true;
            }
            return false;
        }
    }
}
