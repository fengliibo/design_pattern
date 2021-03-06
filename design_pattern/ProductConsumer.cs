﻿
using System;
using System.Collections;
using System.Threading;
namespace ThreadSimple
{
    public class Producer
    {

        ArrayList container = null;

        //得到一个容器

        public Producer(ArrayList container)
        {

            this.container = container;

        }

        //定义一个生产物品的方法装入容器

        public void Product(string name)
        {

            //创建一个新物品装入容器

            Goods goods = new Goods();

            goods.Name = name;

            this.container.Add(goods);



            Console.WriteLine("生产了物品：" + goods.ToString());

        }
        public void Consumer()
        {

            Goods goods = (Goods)this.container[0];



            Console.WriteLine("消费了物品：" + goods.ToString());



            //消费掉容器中的一个物品

            this.container.RemoveAt(0);

        }
        public class Goods
        {

            //物品名称

            private string name;



            public string Name
            {

                get { return name; }

                set { name = value; }

            }

            //重写ToString()

            public override string ToString()
            {

                return "物品名称：" + name;

            }

        }
        class Program
        {
            ArrayList container = new ArrayList();
            Producer producer = null;
            Consumer consumer = null;
            static void Main(string[] args)
            {

                Program p = new Program();

                //创建两个线程并启动

                Thread t1 = new Thread(new ThreadStart(p.ThreadProduct));
                Thread t2 = new Thread(new ThreadStart(p.ThreadConsumption));
                t1.Start();
                //t1.Sleep(1);
                t2.Start();

                Console.Read();

            }

            //定义一个线程方法生产8个物品

            public void ThreadProduct()
            {
                //创建一个生产者
                producer = new Producer(this.container);
                lock (this)  //防止因争夺资源而造成互相等待           {
                    for (int i = 1; i <= 8; i++)
                    {

                        //如果容器中没有就进行生产

                        if (this.container.Count == 0)
                        {
                            //调用方法进行生产
                            producer.Product(i + "");
                            //生产好了一个通知消费者消费
                            Monitor.Pulse(this);
                        }

                        //容器中还有物品等待消费者消费后再生
                        Monitor.Wait(this);
                    }
            }
        }

        //定义一个线程方法消费生产的物品

        public void ThreadConsumption()
        {

            //创建一个消费者

            consumer = new Consumer(this.container);

            lock (this)//防止因争夺资源而造成互相等待               {

                while (true)
                {

                    //如果容器中有商品就进行消费

                    if (this.container.Count != 0)
                    {

                        //调用方法进行消费

                        consumer.Consumption();

                        Monitor.Pulse(this);

                    }

                    //容器中没有商品通知消费者消费

                    Monitor.Wait(this);

                }

        }

    }
}

    }
}
