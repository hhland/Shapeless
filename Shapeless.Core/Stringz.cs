using System;
using System.Collections.Generic;
using System.Linq;
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
            object setValue = null;
            if (type == typeof(String))
            {
                setValue = value;
            }
            else if (new Type[] { typeof(Double), typeof(double), typeof(Nullable<double>), typeof(Nullable<Double>) }.Contains(type)
              )
            {
                setValue = Double.Parse(value);
            }
            else if (new Type[] { typeof(float), typeof(Nullable<float>) }.Contains(type))
            {
                setValue = float.Parse(value);
            }
            else if (new Type[] { typeof(Int32), typeof(int), typeof(Nullable<int>) }.Contains(type)
                  )
            {
                setValue = Int32.Parse(value);
            }
            else if (new Type[] { typeof(Nullable<DateTime>), typeof(DateTime) }.Contains(type))
            {
                setValue = DateTime.Parse(value);
                //setValue = DateTimeUtil.ParseDateTime(value);
            }
            return setValue;
        }
    }
}
