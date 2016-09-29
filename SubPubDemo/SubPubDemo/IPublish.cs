//***********************************************************************************
// 文件名称：IPublilsh.cs
// 功能描述：发布接口
// 数据表：
// 作者：lyevn
// 日期：2016/9/12 20:05:20
// 修改记录：
//***********************************************************************************

using System;

namespace SubPubDemo
{
    /// <summary>
    ///  定义发布接口
    /// </summary>
    public interface IPublish
    {
        /// <summary>
        /// 订阅方法
        /// </summary>
        /// <param name="dealAction">委托方法</param>
        void Subscribe(Action<String> dealAction);

        /// <summary>
        /// 取消订阅方法
        /// </summary>
        /// <param name="dealAction">委托方法</param>
        void DeleteSubscribe(Action<String> dealAction);

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="msg">信息</param>
        void Publish(String msg);
    }
}
