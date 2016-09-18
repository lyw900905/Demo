//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//namespace SubPubDemo
//{
//    using Common.Interface;
//    /// <summary>
//    /// 订阅器
//    /// </summary>
//    public class SubPubComponet : ISubscribe, IPublish
//    {
//        private string _subName;
//        //event SubscribeHandle subscribeEvent;
//        //event SubscribeHandle ISubscribe.SubscribeEvent
//        //{
//        //    add { subscribeEvent += value; }
//        //    remove { subscribeEvent -= value; }
//        //}

//        public PublishHandle PublishEvent;
//        event PublishHandle IPublish.PublishEvent
//        {
//            add { PublishEvent += value; }
//            remove { PublishEvent -= value; }
//        }

//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        /// <param name="subName">订阅器名称</param>
//        public SubPubComponet(string subName)
//        {
//            this._subName = subName;
//            //PublishEvent += new PublishHandle(Notify);
//        }

       
//        /// <summary>
//        /// 通知方法
//        /// </summary>
//        /// <param name="str">通知消息</param>
//        public void Notify(string str)
//        {
//            //if (subscribeEvent != null)
//            //{
//            //    subscribeEvent.Invoke(string.Format("消息来源{0}:消息内容：{1}", _subName, str));
//            //}
//        }
//    }
//}
