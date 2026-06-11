using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleWPFDemo
{
    public class DoWork : IDoWork
    {
        public void WorkWork()
        {
            Console.WriteLine("I am working");
        }
    }
}
