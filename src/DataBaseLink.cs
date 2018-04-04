using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLexperiment_dome_1
{
    class MyDataBaseLink
    {
        private static string DataBaseName = "menstruation";//数据库名称
        private string constr = "server=localhost;User Id=root;password=0514;Database="+ DataBaseName;//数据库连接信息

        private bool executeSQLsentence_add(string SQLsentence)//执行插入、删除语句
        {
            try
            {
                MySqlConnection mycon = new MySqlConnection(constr);
                mycon.Open();
                MySqlCommand mycmd = new MySqlCommand(SQLsentence, mycon);
                if (mycmd.ExecuteNonQuery() > 0)
                {
                    mycon.Close();
                    return true;
                }
                mycon.Close();
                return false;
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                //Console.WriteLine("数据库异常，请检查输入");
                return false;
            }
        }

        public bool AddData(string data)//插入开始时间
        {
            String SQLsentence = "insert into menstruation(startData) values('" + data + "')";
            Console.WriteLine(SQLsentence);
            executeSQLsentence_add(SQLsentence);
            if (executeSQLsentence_add(SQLsentence))
            {
                Console.WriteLine("数据插入成功！");
                return true;
            }
            else
            {
                Console.WriteLine("数据插入失败！");
                return false;
            }
        }

        private int getDays(string start, string end)//计算时间差
        {
            DateTime d1 = Convert.ToDateTime(start);
            DateTime d2 = Convert.ToDateTime(end);
            DateTime d3 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", d1.Year, d1.Month, d1.Day));
            DateTime d4 = Convert.ToDateTime(string.Format("{0}-{1}-{2}", d2.Year, d2.Month, d2.Day));
            int days = (d4 - d3).Days;
            if (days<=0)
            {
                return -1;
            }
            else if (days>=30)
            {
                return -2;
            }
            return days;
        }

        public bool AddData(string start, string end)//添加数据并计算出时间差
        {
            if (check(start))
            {
                //Console.WriteLine("已存在");
                if (check(start, end))
                {
                    Console.WriteLine("已存在，请不要重复添加");
                    return false;
                }
                else
                {
                    int days = getDays(start, end);
                    if (days == 1)
                    {
                        Console.WriteLine("结束时间可能不迟于开始时间，请重新输入");
                        return false;
                    }
                    else if (days == 2)
                    {
                        Console.WriteLine("输入时间跨度过大，请检查数据重新输入");
                        return false;
                    }
                    String SQLsentence = "update menstruation set endData = '" + end + "' where startData =  '" + start + "'";
                    Console.WriteLine(SQLsentence);
                    if (executeSQLsentence_add(SQLsentence))
                    {
                        Console.WriteLine("数据插入成功！");
                    }
                    else
                    {
                        Console.WriteLine("数据插入失败！");
                        return false;
                    }
                    SQLsentence = "update menstruation set takeTime = '" + days + "' where startData =  '" + start + "'";
                    Console.WriteLine(SQLsentence);
                    if (executeSQLsentence_add(SQLsentence))
                    {
                        Console.WriteLine("数据插入成功！");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("数据插入失败！");
                        return false;
                    }
                }
            }
            else
            {
                int days = getDays(start, end);
                if (days==1)
                {
                    Console.WriteLine("结束时间可能不迟于开始时间，请重新输入");
                    return false;
                }
                else if (days==2)
                {
                    Console.WriteLine("输入时间跨度过大，请检查数据重新输入");
                    return false;
                }
                
                String SQLsentence = "insert into menstruation values('" + start + "','" + end + "','" + days.ToString() + "')";
                Console.WriteLine(SQLsentence);
                if (executeSQLsentence_add(SQLsentence))
                {
                    Console.WriteLine("数据插入成功！");
                    return true;
                }
                else
                {
                    Console.WriteLine("数据插入失败！");
                    return false;
                }
            }
        }

        private bool check(string start)//检查是否有以存在的开始时间
        {
            try
            {
                String SQLsentence = "select * from menstruation where startData='"+start+"'";
                Console.WriteLine(SQLsentence);
                MySqlConnection mycon = new MySqlConnection(constr);
                mycon.Open();
                MySqlCommand mycmd = new MySqlCommand(SQLsentence, mycon);
                String a = mycmd.ExecuteScalar().ToString();
                Console.WriteLine(a);
                mycon.Close();
                return true;//存在
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                return false;//不存在
            }
        }

        private bool check(string start,string end)//检查是否存在开始时间和结束时间都存在的记录
        {
            try
            {
                String SQLsentence = "select * from menstruation where startData='" + start + "' and endData='" + end + "'";
                Console.WriteLine(SQLsentence);
                MySqlConnection mycon = new MySqlConnection(constr);
                mycon.Open();
                MySqlCommand mycmd = new MySqlCommand(SQLsentence, mycon);
                String a = mycmd.ExecuteScalar().ToString();
                Console.WriteLine(a);
                mycon.Close();
                return true;//存在
            }
            catch (System.NullReferenceException)
            {
                return false;//不存在
            }
            

        }

        public void show()//显示数据库的内容
        {
            MySqlConnection sqlCnn = new MySqlConnection();
            sqlCnn.ConnectionString = constr;//连接字符串
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = sqlCnn;
            sqlCmd.CommandText = "select * from menstruation";
            sqlCnn.Open();
            //创建数据库操作对象
            MySqlDataReader rec = sqlCmd.ExecuteReader();
            while (rec.Read())
            {
                DateTime d1 = rec.GetDateTime(0);
                string d3 = d1.Year + "年" + d1.Month + "月" + d1.Day + "日";
                try
                {
                    DateTime d5 = rec.GetDateTime(1);
                    string d6 = d5.Year + "年" + d5.Month + "月" + d5.Day + "日";
                }
                catch (Exception)
                {
                    Console.WriteLine("{0}\t \t 0天\t\n", d3);
                    continue;
                }
                DateTime d2 = rec.GetDateTime(1);
                string d4 = d2.Year + "年" + d2.Month + "月" + d2.Day + "日";
                Console.WriteLine("{0}\t {1}\t {2}天\t\n", d3, d4, rec.GetValue(2));
            }
            sqlCnn.Close();
        }

        public bool DeleteData(string start)//删除某条记录
        {
            if (check(start))
            {
                string SQLsentence = "delete from menstruation where startData = '"+start+"'";
                Console.WriteLine(SQLsentence);
                if (executeSQLsentence_add(SQLsentence))
                {
                    Console.WriteLine("数据删除成功！");
                    return true;
                }
                else
                {
                    Console.WriteLine("数据删除失败！");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("没有该记录");
                return false;
            }
        }
    }
}
