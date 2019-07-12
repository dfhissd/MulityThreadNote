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
            //Thread t1 = new Thread(new ThreadStart(PrintNumbers));//无参数的委托
            //t1.Start();
            //Thread t2 = new Thread(new ParameterizedThreadStart(PrintNumbers));//有参数的委托
            //t2.Start(10);
            //Console.ReadKey();
            //=====================线程阻塞======================//
            //Thread t1 = new Thread(PrintNumbersWithDelay);
            //t1.Start();
            //PrintNumbers();
            //Console.ReadLine();
            //=====================线程等待======================//
            Console.WriteLine("Starting...");
            Thread th = new Thread(PrintNumbersWithDelay2);
            th.Start();
            th.Join();  //使用join等待th完成
            PrintNumbers2();
            Console.WriteLine("THread Complete");
            Console.ReadLine();

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
        //=====================线程阻塞======================//
        static void PrintNumbersWithDelay()
        {
            Console.WriteLine("PrintNumbersWithDelay Starting...");
            for (int i = 0; i < 10; i++) {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.Write(i);
            }
            Console.WriteLine();
        }
        //=====================线程等待======================//
        static void PrintNumbers2()
        {
            Console.WriteLine("PrintNumbers2 Starting...");
            for (int i = 0; i < 10; i++)
                Console.WriteLine(i);
        }
        static void PrintNumbersWithDelay2()
        {
            Console.WriteLine("PrintNumbersWithDelay2 Starting...");
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(i);
            }
        }
    }
}
