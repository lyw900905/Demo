﻿
/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： 发布者实现
** Ver.:  V1.0.0
*********************************************************************************/

namespace SubPubDemo
{
    /// <summary>
    /// 发布者
    /// </summary>
    public class Publisher : IPublish
    {
        private string pubContent; //todo: 缺少代码注释
        /// <summary>
        /// 发布内容
        /// </summary>
        public string PubContent //todo:summary和上面的字段之间缺少一个空行
        {
            get
            {
                return pubContent;
            }
            set
            {
                pubContent = value;
            }
        }

        //todo：缺少代码注释
        public event PublishHandle publishEvent;

        //todo:无用代码要删除，保证代码整洁
        //event PublishHandle IPublish.PublishEvent
        //{
        //    add { publishEvent += value; }
        //    remove { publishEvent -= value; }
        //}

        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="publisherName">发布者名称</param>
        //public Publisher(string publisherName)
        //{
        //   this.publisherName = publisherName;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ph"></param>
        //public void AddPublisher(PublishHandle ph)
        //{
        //    publishEvent += ph;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="ph"></param>
        //public void RemovePublisher(PublishHandle ph) 
        //{
        //    publishEvent -= ph;
        //}

        ///// <summary>
        ///// 通知
        ///// </summary>
        ///// <param name="str">通知信息</param>
        //public void Notify(string str)
        //{
        //    publishEvent();
        //}

        /// <summary>
        /// 通知
        /// </summary>
        public void Notify()
        {
            publishEvent(); //todo:此处没有判断publishEvent是否为null，在无事件注册时，为null
        }
    }
}
