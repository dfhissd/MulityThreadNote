using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MulityThreadNote
{
    class Program
    {
        static void Main(string[] args)
        {
            //=====================创建线程======================//
            Thread t1 = new Thread(new ThreadStart(PrintNumbers));//无参数的委托
            t1.Start();

            Thread t2 = new Thread(new ParameterizedThreadStart(PrintNumbers));//有参数的委托
            t2.Start(10);
            Console.ReadKey();
            

        }
        //=====================创建线程======================//
        static void PrintNumbers()
        {
            Console.WriteLine("PrintNumbers Starting...");
            for (int i = 0; i < 10; i++)
                Console.Write(i);
            Console.WriteLine();
        }
        //注意：要使用ParameterizedThreadStart，定义的参数必须为object
        static void PrintNumbers(object count)
        {
            Console.WriteLine("PrintNumbers Starting...");
            for (int i = 0; i < Convert.ToInt32(count); i++)
                Console.Write(i);
            Console.WriteLine();
        }



    }
}
