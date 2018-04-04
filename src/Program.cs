using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SQLexperiment_dome_1
{
    
    class Program
    {
        static private string dataFormatting(string ss)
        {
            char[] cc = ss.ToCharArray();
            ss = cc[0].ToString()+cc[1].ToString()+cc[2].ToString()+cc[3].ToString()+"-"+cc[4].ToString()+cc[5].ToString()+"-"+cc[6].ToString()+cc[7].ToString();
            return ss;
        }
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("1.输入信息1,2.输入信息2，3.查看信息,4.退出");
                    Int16 sgin = Convert.ToInt16(Console.ReadLine());
                    Console.WriteLine(sgin);
                    switch (sgin)
                    {
                        case 1:
                            {
                                string s1 = Convert.ToString(Console.ReadLine());
                                s1 = dataFormatting(s1);
                                Console.WriteLine(s1);
                                MyDataBaseLink myDataBaseLink = new MyDataBaseLink();
                                myDataBaseLink.AddData(s1);
                                break;
                            }
                        case 2:
                            {
                                string s1 = Convert.ToString(Console.ReadLine());
                                s1 = dataFormatting(s1);
                                Console.WriteLine(s1);
                                string s2 = Convert.ToString(Console.ReadLine());
                                s2 = dataFormatting(s2);
                                Console.WriteLine(s2);
                                MyDataBaseLink myDataBaseLink = new MyDataBaseLink();
                                myDataBaseLink.AddData(s1,s2);
                                break;
                            }
                        case 3:
                            {
                                MyDataBaseLink myDataBaseLink = new MyDataBaseLink();
                                myDataBaseLink.show();
                                break;
                            }
                        case 4:
                            {
                                Environment.Exit(0);
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
                Environment.Exit(0);
            }
        }
    }
}
