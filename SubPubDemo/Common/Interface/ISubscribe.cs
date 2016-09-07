using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interface
{
    // 定义订阅事件
    public delegate void SubscribeHandle(string str);

    /// <summary>
    ///  定义订阅接口
    /// </summary>
    public interface ISubscribe
    {
        event SubscribeHandle SubscribeEvent;
    }
}
