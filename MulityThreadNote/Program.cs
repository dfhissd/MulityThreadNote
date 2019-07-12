using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

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
            //Console.WriteLine("Starting...");
            //Thread th = new Thread(PrintNumbersWithDelay2);
            //th.Start();
            //th.Join();  //使用join等待th完成
            //PrintNumbers2();
            //Console.WriteLine("THread Complete");
            //Console.ReadLine();

            //=====================终止线程======================//
            //Console.WriteLine("Starting Program...");
            //Thread t1 = new Thread(PrintNumbersWithDelay2);
            //t1.Start();
            //Thread.Sleep(TimeSpan.FromSeconds(6));
            //t1.Abort();     //使用Abort()终止线程
            //Console.WriteLine("Thread t1 has been aborted");
            //Thread t2 = new Thread(PrintNumbers2);
            ////t2.Start();
            //PrintNumbers2();
            //Console.ReadLine();

            //=====================检测线程状态=====================//
            //Console.WriteLine("Start Program...");
            //Thread t1 = new Thread(Status);
            //Thread t2 = new Thread(OnlySleep);
            //Console.WriteLine(t1.ThreadState.ToString());
            //t2.Start();
            //t1.Start();
            //for (int i = 0; i < 20; i++)
            //    Console.WriteLine(t1.ThreadState.ToString());
            //Thread.Sleep(TimeSpan.FromSeconds(6));
            //t1.Abort();
            //Console.WriteLine("thread t1 has been aborted");
            //Console.WriteLine(t1.ThreadState.ToString());
            //Console.WriteLine(t2.ThreadState.ToString());
            //Console.ReadLine();

            //=====================线程优先级=====================//
            //Console.WriteLine("Current thread priority: {0}", Thread.CurrentThread.Priority);
            //Console.WriteLine("Running on all cores available");//获取线程状态
            //RunThreads();

            //Thread.Sleep(TimeSpan.FromSeconds(2));
            //Console.WriteLine("Running on a single Core");
            ////让操作系统的所有线程运行在单个CPU核心上
            //Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(1);
            //RunThreads();
            //Console.ReadLine();

            //=====================前台线程和后台线程=====================//
            //var sampleForground = new Threadsample(10);
            //var sampleBackground = new Threadsample(20);
            //var t1 = new Thread(sampleForground.CountNumbers);
            //t1.Name = "ForgroundThread";    //没有明确声明的均为前台线程
            //var t2 = new Thread(sampleBackground.CountNumbers);
            //t2.Name = "BackgroundThread";
            //t2.IsBackground = true;     //设置为后台线程

            //t1.Start();
            //t2.Start();
            //Console.ReadKey();

            //=====================向线程传递参数=====================//
            ThreadSample2 sample = new ThreadSample2(5);

            Thread t1 = new Thread(sample.CountNumbers);
            t1.Name = "ThreadOne";
            t1.Start();
            t1.Join();
            Console.WriteLine("-----------------------");

            Thread t2 = new Thread(Count);
            t2.Name = "ThreadTwo";
            t2.Start(3);
            t2.Join();
            Console.WriteLine("-----------------------");

            //使用lambda表达式引用另一个C#对象的方式被称为闭包。当在lambda表达式中使用任何局部变量时，
            //C#会生成一个类，并将该变量作为该类的一个属性，但是我们无须定义该类，
            //C#编译器会自动帮我们实现
            Thread t3 = new Thread(() => CountNumbers(5));
            t3.Name = "ThreadThree";
            t3.Start();
            t3.Join();
            Console.WriteLine("--------------------------");

            int i = 10;
            Thread t4 = new Thread(() => PrintNumber(i));
            

            i = 20;
            Thread t5 = new Thread(() => PrintNumber(i));
            t4.Start();
            t5.Start();
            //t4, t5都会输出20，因为t4,t5没有start之前i已经变成20了
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
        //=====================检测线程状态=====================//
        private static void Status()
        {
            Console.WriteLine("Staring...");
            Console.WriteLine(Thread.CurrentThread.ToString());//获取当前线程状态
            for (int i = 0; i < 10; i++) {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(i);
            }
        }
        private static void OnlySleep()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }
        //=====================线程优先级=====================//
        private static void RunThreads()
        {
            var sample = new ThreadSample();

            var t1 = new Thread(sample.CountNumbers);
            t1.Name = "Thread One";
            var t2 = new Thread(sample.CountNumbers);
            t2.Name = "Thread Two";

            t1.Priority = ThreadPriority.Highest;   //使用priority设置线程的优先级
            t2.Priority = ThreadPriority.Lowest;
            t1.Start();
            t2.Start();

            Thread.Sleep(TimeSpan.FromSeconds(2));
            sample.stop();
        }
        class ThreadSample
        {
            private bool _isStopped = false;
            public void stop()
            {
                _isStopped = true;
            }

            public void CountNumbers()
            {
                long counter = 0;
                while (!_isStopped) {
                    counter++;
                }
                Console.WriteLine("{0} with {1} priority has a count = {2}", Thread.CurrentThread.Name, Thread.CurrentThread.Priority, counter.ToString("NO"));
            }
        }
        //=====================前台线程和后台线程=====================//
        class Threadsample
        {
            private readonly int _iteration;

            public Threadsample(int iteration)
            {
                _iteration = iteration;
            }

            public void CountNumbers()
            {
                for (int i = 0; i < _iteration; i++) {
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    Console.WriteLine("{0} prints {1}", Thread.CurrentThread.Name, i);
                }

            }
        }
        //=====================向线程传递参数=====================//
        static void Count(object iterations)
        {
            CountNumbers((int)iterations);

        }
        static void CountNumbers(int iteration)
        {
            for (int i = 1; i <= iteration; i++) {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine("{0} prints {1}", Thread.CurrentThread.Name, i);
            }
        }
        static void PrintNumber(int number)
        {
            Console.WriteLine(number);
        }
        class ThreadSample2
        {
            private readonly int _iteration;

            public ThreadSample2(int iteration)
            {
                _iteration = iteration;
            }

            public void CountNumbers()
            {
                for (int i = 1; i <= _iteration; i++){
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    Console.WriteLine("{0} prints {1}", Thread.CurrentThread.Name, i);
                }
            }

        }
    }
}
