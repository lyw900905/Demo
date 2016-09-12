using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interface
{
    // 定义发布事件
    public delegate void PublishHandle(string str); ////todo:委托注释同函数注释

    /// <summary>
    ///  定义发布接口
    /// </summary>
    public interface IPublish
    {
        //todo:缺少注释
        event PublishHandle PublishEvent;

        void Notify(string str);
    }
}
