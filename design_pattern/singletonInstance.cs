using System;
namespace Design_Pattern
{
    public class singletonInstance
    {
        private static singletonInstance instance = null;
        private static readonly object threadSafeLock = new object();
        public static singletonInstance Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (threadSafeLock)
                    {
                        if (instance == null)
                        {
                            instance = new singletonInstance();
                        }

                    }
                }
                return instance;
            }

        }
        public void Hello()
        {
            Console.WriteLine("Hello");
        }
        public void Hi()
        {
            Console.WriteLine("Hi");
        }
        public singletonInstance()
        {
        }
    }
}
