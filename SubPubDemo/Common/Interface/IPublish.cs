using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interface
{
    // 定义发布事件
    public delegate void PublishHandle(string str);

    /// <summary>
    ///  定义发布接口
    /// </summary>
    public interface IPublish
    {
        event PublishHandle PublishEvent;

        void Notify(string str);
    }
}
