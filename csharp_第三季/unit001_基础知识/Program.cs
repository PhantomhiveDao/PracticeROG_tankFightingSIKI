using System;
using System.Threading;

namespace unit001_基础知识
{
    class Program
    {
        static void Main(string[] args)
        {
        //    int a = 34;
        //    double b = a;//自动类型转换-适用情况：右侧取值范围小于左侧
        //    double c = 23;
        //    a = (int)c;//强制类型转换时会有数据丢失；当数据类型存不下时，会溢出。

        //    Father f = new Son();//将son强制类型转换为Father

        //    Son s = (Son)f;
        //    //Son s = f as Son;
        //    s.SonMethod();
        //    Thread t = Thread.CurrentThread;//获取到当前的线程
        //    t.Name = "mainThread";

        //    Console.WriteLine((t.Name));

            ThreadStart start = new ThreadStart(ChildThreadMethod);

            Thread ChildThead = new Thread(start);

            ChildThead.Start();
            Console.WriteLine("MainThead");

           
           
            Thread.Sleep(500);
            isrun = false;

            
        }

        private static bool isrun = true;
        private static void ChildThreadMethod()
        {
            Console.WriteLine("子线程进行中！");
            while (isrun)
            {
                Console.WriteLine("我是子线程");
            }
        }

    }
    class Father
    {
        
    }
    class Son : Father
    {
        public void SonMethod()
        { }
    }

}
