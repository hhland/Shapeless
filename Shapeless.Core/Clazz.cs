using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Collections.Specialized;

namespace Shapeless.Core
{
    public class Clazz
    {

       

        public static void from<T>(T obj, XmlNode node, XmlNamespaceManager xnm, string prefix, string subfix)
        {
            Type type = obj.GetType();
            IDictionary<string, MethodInfo> setters = setterMethods(type);
            foreach (string key in setters.Keys)
            {
                MethodInfo method = setters[key];
                string query = prefix + key + subfix;
              
                    string value = null;
                    if (xnm != null)
                    {
                        value = node.SelectSingleNode(query, xnm).InnerText;
                    }
                    else
                    {
                        value = node.SelectSingleNode(query).InnerText;
                    }
                    method.Invoke(obj, new object[] { value });
                
            }
            
        }

        public static IDictionary<string, MethodInfo> methods(Type type)
        {
            IDictionary<string, MethodInfo> methods = new Dictionary<string, MethodInfo>();
            Type baseType = type;
            while (baseType != typeof(Object) && baseType != typeof(object))
            {
                MethodInfo[] tempMethods = baseType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                foreach (MethodInfo tempMethod in tempMethods)
                {

                    string name = tempMethod.Name.Substring(4);
                    methods.Add(name, tempMethod);
                }
                baseType = baseType.BaseType;
            }

            return methods;
        }

        public static IDictionary<string, MethodInfo> setterMethods(Type type)
        {
            return Clazz.methods(type).Values.Where(mth => mth.Name.StartsWith("set_")).ToDictionary(mth => mth.Name);
        }

        public static IDictionary<string, MethodInfo> getterMethods(Type type)
        {
            return Clazz.methods(type).Values.Where(mth => mth.Name.StartsWith("get_")).ToDictionary(mth => mth.Name);
        }



        public static void from  (object obj, NameValueCollection nvc, string prefix, string subfix)
        {
            Type type = obj.GetType();
            IDictionary<string, MethodInfo> setters = setterMethods(type);
            foreach (string key in setters.Keys)
            {
                string query = prefix + key + subfix;
                query = query.StartsWith("_") ? query.Substring(1) : query;
                
                    string value = nvc[query];
                    if (value == null) continue;
                    attr(ref obj, query, value);
                
                
            }

        }

        public static void attr(ref object obj, string name, object value)
        {
            MethodInfo method = obj.GetType().GetMethod("set_" + name);
            method.Invoke(obj, new object[]{value});
        }

        public static object attr(ref object obj, string name)
        {

            MethodInfo method = obj.GetType().GetMethod("get_" + name);
            return method.Invoke(obj, new object[] { });

        }

        

        






        public static bool isEqual <T> (T t,params T[] ts)
        {
            foreach (T t0 in ts)
            {
                if (!t.Equals(t0)) return false;
            }
            return true;
        }

        public static bool isNotEqual<T>(T t, params T[] ts)
        {
            return !isEqual(t,ts);
        }


        public static bool isList<T>(object obj)
        {
            if (obj == null) return false;
            return obj is IList
                    || obj is ArrayList
                    || obj is List<T>
                    || obj is LinkedList<T>
                ;
        }

        public static bool isDictionary<K, V>(object obj)
        {
            if (obj == null) return false;
            Type type = obj.GetType();
            return typeof(Dictionary<K, V>).Equals(type);
        }


        public static bool isNullOrEmpty(params string[] strs)
        {
            foreach (string str in strs)
            {
                if (string.IsNullOrEmpty(str)) return false;
            }
            return true;
        }

       

    }
}
