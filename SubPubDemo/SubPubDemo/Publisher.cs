//***********************************************************************************
// 文件名称：Publisher.cs
// 功能描述：发布者
// 数据表：
// 作者：Lyevn
// 日期：2016/9/12 20:10:20
// 修改记录：
//***********************************************************************************

using System;
using System.Collections.Generic;

namespace SubPubDemo
{
    /// <summary>
    /// 发布者
    /// </summary>
    public class Publisher : IPublish
    {
        /// <summary>
        /// 用户订阅方法列表操作锁
        /// </summary>
        private Object dictionaryLock = new Object();

        /// <summary>
        /// 存储用户订阅的方法列表
        /// </summary>
        private Dictionary<String, Action<string>> subscribeActionDictionary = new Dictionary<string, Action<string>>();

        /// <summary>
        /// 订阅方法
        /// </summary>
        /// <param name="dealAction">处理事件委托方法</param>
        public void Subscribe(String subscriberName, Action<String> dealAction)
        {
            lock (dictionaryLock)
            {            
                // 判断是否已经存在订阅内容，不存在则添加
                if (!subscribeActionDictionary.ContainsKey(subscriberName))
                {
                    subscribeActionDictionary[subscriberName] = dealAction;
                }
            }
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="dealAction">处理事件委托方法</param>
        public void DeleteSubscribe(String subscriberName, Action<String> dealAction)
        {
            lock (dictionaryLock)
            {
                // 判断是否存在要取消订阅的事件方法，存在则取消事件
                if (subscribeActionDictionary.ContainsKey(subscriberName))
                {
                    subscribeActionDictionary.Remove(subscriberName);
                }
            }
        }

        /// <summary>
        /// 发布信息
        /// </summary>
        /// <param name="message">信息内容</param>
        public void Publish(String message)
        {
            // 获取订阅列表中的键值
            var keys = subscribeActionDictionary.Keys;

            foreach (var key in keys)
            {
                // 通过key值来通知订阅的方法信息
                subscribeActionDictionary[key].Invoke(message);
            }
        }
    }
}
