﻿//***********************************************************************************
// 文件名称：JSonHelper.cs
// 功能描述：json序列化与反序列化操作类
// 数据表：
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Common.Helper
{
    /// <summary>
    /// json序列化帮助类(未使用，现采用Newtonsoft.Json.dll提供的序列化方法)
    /// </summary>
    public class JSonHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>json字符串</returns>
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
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>反序列化对象</returns>
        public static T Deserialize<T>(String json)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                T obj = (T)serializer.ReadObject(ms);
                ms.Close();

                return obj;
            }
        }

    }
}
