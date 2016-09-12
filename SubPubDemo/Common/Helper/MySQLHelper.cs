/********************************************************************************
** auth： lyw
** date： 2016-09-12
** desc： 数据库操作帮助类
** Ver.:  V1.0.0
*********************************************************************************/

namespace Common.Helper
{
    using MySql.Data.MySqlClient;
    using System.Data;

    /// <summary>
    /// 数据库操作帮助类
    /// </summary>
    public class MySQLHelper
    {
        static readonly string constr = string.Empty;

        static MySQLHelper()
        {
            constr = "Server=127.0.0.1; Uid=root; Pwd=1234; Database=TestDemoDb";
        }

        /// <summary>
        /// 执行不带参数的sql命令
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>返回影响结果</returns>
        public static int ExcuteNonQuery(string cmdText)
        {
            int count = 0;

            using (MySqlConnection mycon = new MySqlConnection(constr))
            {
                mycon.Open();
                MySqlCommand mycmd = new MySqlCommand(cmdText);
                count = mycmd.ExecuteNonQuery();
                mycon.Close();
            }

            return count;
        }

        /// <summary>
        /// 执行带Parameter参数的sql命令
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParas">参数信息</param>
        /// <returns>返回影响结果</returns>
        public static int ExcuteNonQuery(string cmdText, MySqlParameter[] cmdParas)
        {
            int count = 0;

            using (MySqlConnection mycon = new MySqlConnection(constr))
            {
                mycon.Open();
                MySqlCommand mycmd = new MySqlCommand(cmdText);
                if (cmdParas != null && cmdParas.Length > 0)
                {
                    mycmd.Parameters.AddRange(cmdParas);
                }
                count = mycmd.ExecuteNonQuery();
                mycon.Close();
            }

            return count;
        }
        
        /// <summary>
        /// 执行不带参数sql语句，返回DataTable数据信息
        /// </summary>
        /// <param name="cmdText">sql指令</param>
        /// <returns>DataTable表数据信息</returns>
        public static DataTable GetDataTable(string cmdText)
        {
            DataTable db = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(constr))
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdText, constr);
                adapter.Fill(db);
                conn.Close();
            }

            return db;
        }

        /// <summary>
        /// 执行带MySqlParameter参数的sql语句，返回结果的首行首列的object值
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="cmdParas">命令参数</param>
        /// <returns>结果的首行首列的object值</returns>
        public static object ExecuteScalar(string cmdText, MySqlParameter[] cmdParas)
        {
            object obj = null;

            using (MySqlConnection mycon = new MySqlConnection(constr))
            {
                mycon.Open();
                MySqlCommand mycmd = new MySqlCommand(cmdText, mycon);
                if (cmdParas != null && cmdParas.Length > 0)
                {
                    mycmd.Parameters.AddRange(cmdParas);
                }
                obj = mycmd.ExecuteScalar();
                mycon.Close();
            }

            return obj;
        }
    }
}
