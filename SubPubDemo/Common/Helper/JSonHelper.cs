/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： json序列化帮助类
** Ver.:  V1.0.0
*********************************************************************************/

using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Common.Helper
{
    /// <summary>
    /// json序列化帮助类
    /// </summary>
    public class JSonHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)//todo:这边规范要求普通类型不能用小写的，string=>String int=>Int32,Int64
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType()); //todo:你可以检查一下这个序列化结果，实际上是有很多垃圾信息的。可以再查资料确认有没有其他序列化方案
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            T obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }

    }
}
