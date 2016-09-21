/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： json序列化帮助类
** Ver.:  V1.0.0
*********************************************************************************/
 
using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Common.Helper
{
    /// <summary>
    /// json序列化帮助类(未使用，采用Newtonsoft.Json.dll提供的序列化方法)
    /// </summary>
    public class JSonHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static String Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            String retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(String json)
        {
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            T obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }

    }
}
