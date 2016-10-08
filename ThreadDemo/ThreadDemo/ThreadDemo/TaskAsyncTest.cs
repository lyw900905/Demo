//***********************************************************************************
// 文件名称：TaskAsyncTest.cs
// 功能描述：TaskAsyncTest测试类
// 数据表：
// 作者：Lyevn
// 日期：2016/10/08 14:49:20
// 修改记录：
//***********************************************************************************

using System;
using System.Threading.Tasks;

namespace ThreadDemo
{
    /// <summary>
    /// TaskAsync测试类
    /// </summary>
    public class TaskAsyncTest
    {
        /// <summary>
        /// 开始TaskAsync测试方法
        /// </summary>
        public static void StartTest()
        {
            //RunAsync(Function, CallBack);
            AsyncMethod();
        }

        /// <summary>
        /// 将一个方法function异步运行，在执行完毕时执行回调callback
        /// </summary>
        /// <param name="function">异步方法，该方法没有参数，返回类型必须是void</param>
        /// <param name="callback">异步方法执行完毕时执行的回调方法，该方法没有参数，返回类型必须是void</param>
        //private static async void RunAsync(Action function, Action callback)
        //{
        //    Func<Task> taskFunc = () =>
        //    {
        //        return Task.Run(() =>
        //        {
        //            function();
        //        });
        //    };

        //    await taskFunc();

        //    if (callback != null)
        //    {
        //        callback();
        //    }
        //}

        /// <summary>
        /// 将一个方法function异步运行，在执行完毕时执行回调callback 
        /// </summary>
        /// <typeparam name="TResult">异步方法的返回类型</typeparam>
        /// <param name="function">异步方法，该方法没有参数，返回类型必须是TResult</param>
        /// <param name="callback">异步方法执行完毕时执行的回调方法，该方法参数为TResult，返回类型必须是void</param>
        //private static async void RunAsync<TResult>(Func<TResult> function, Action<TResult> callback)
        //{
        //    Func<Task<TResult>> taskFunc = () =>
        //    {
        //        return Task.Run(() =>
        //        {
        //            return function();
        //        });
        //    };

        //    TResult tResult = await taskFunc();

        //    if (callback != null)
        //    {
        //        callback(tResult);
        //    }
        //}

        private static async void AsyncMethod()
        {
            Console.WriteLine("开始异步代码");

            var result = await TestMethod();
        }

        /// <summary>
        /// 异步调用测试方法
        /// </summary>
        /// <returns></returns>
        private static async Task<int> TestMethod()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("异步执行"+i.ToString());

                await Task.Delay(1000);
            }

            return 0;
        }

        /// <summary>
        /// 异步执行不带返回结果的方法
        /// </summary>
        private static async void Function()
        {
            Console.WriteLine("this is Function!");
        }

        /// <summary>
        /// 异步方法执行完成结果回调函数
        /// </summary>
        private static void CallBack()
        {
            Console.WriteLine("This is CallBack!");
        }
    }
}
