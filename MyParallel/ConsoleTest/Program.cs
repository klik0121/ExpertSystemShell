using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyParallel;
using System.Diagnostics;
using MyParallel.ReadWriteLock;
using System.Threading;

namespace ConsoleTest
{
    class Program
    {
        /// <summary>
        /// Имитация работы студентов и преподавателя над общим проектом.
        /// Преподаватель действует по следующему принципу: он делит общий объём работ на 
        /// отдельные версии (номер весии до точки),
        /// каждая из которых в свою очередь состоит из нескольких работ (номер работы 
        /// после точки). Для каждой версии он даёт студентам время выполнить работы. На данном этапе 
        /// преподаватель надеется на студентов и не проверяет их работу.
        /// Когда он выдаст все задания, он устанавливает дедлайн, что означает,
        /// что проект должен быть закончен как можно скорее.
        /// С этого момента преподаватель начинает проверять состояние проекта.
        /// Студенты действуют по следующему принципу:
        /// будучи ленивыми от природы, они предпочитает отдохнуть, если работ не так много (меньше 10) 
        /// или до дедлайна далеко (project.deadline = false).
        /// Если они работают, то они по очереди забирают работы из списка доступных и выполняют их.
        /// Если же работ не осталось, то ничего не остаётся, кроме как отдохнуть.
        /// Когда выполнена последняя работа и преподаватель увидел этот факт, проект завершается.
        /// </summary>
        public static void Main(string[] args)
        {
            object locker = new object();
            Project project = new Project();
            Thread[] threads = new Thread[4];
            Stopwatch sw = new Stopwatch();
            double realTime = 0;
            threads[0] = new Thread(() => 
                {
                    sw.Start();
                    Random rnd = new Random();
                    int versionAmt = rnd.Next(3, 8);
                    for(int i = 0; i < versionAmt; i++)
                    {
                        int taskAmt = rnd.Next(4, 7);
                        for(int j = 0; j < taskAmt; j++)
                        {
                            string task = string.Format("задание v {0}.{1}", i + 1, j + 1);
                            project.AppendTask(task);
                            Console.WriteLine(string.Format("Преподаватель добавил {0}.", task));
                            Thread.Sleep(500); //преподавателю нужно время, чтобы написать ещё задание
                        }
                        Thread.Sleep(3000); //даёт студентам время выполнить задания
                    }
                    project.SetDeadLine(); //устаналивает дедлайн
                    while(!project.IsFinished())
                    {
                        Console.WriteLine("Преподаватель проверил, но проект ещё не завершён.");
                        Thread.Sleep(1500);                       
                    }
                }
            );
            for(int i = 1; i < threads.Length; i++)
            {
                threads[i] = new Thread((arg) =>
                    {
                        int num = (int)((object[])arg)[0]; //номер студента
                        while(!project.IsFinished())
                        {
                            if(project.TimeToWork()) //время работать?
                            {
                                string task = null;
                                if(project.GetCurrentTask(out task)) //остались задания?
                                {
                                    Console.WriteLine(string.Format("Студент {0} взял задание {1}", num, task));
                                    Random rnd = new Random();
                                    int timeForWork = (int)rnd.NextDouble() * 2000 + 1000;
                                    lock (locker)
                                    {
                                        realTime += timeForWork;
                                    }
                                    Thread.Sleep(timeForWork);
                                    project.CompleteTask();
                                    Console.WriteLine(string.Format("Студент {0} выполнил задание {1}", num, task));
                                }
                                else
                                {
                                    Console.WriteLine(string.Format("Студенту {0} не осталось заданий.", num));
                                    Thread.Sleep(3000);
                                }
                            }
                            else
                            {
                                Console.WriteLine(string.Format("Студент {0} решил отдохнуть.", num));
                                Thread.Sleep(3000);
                            }
                        }
                    });
            }

            threads[0].Start();
            for(int i = 1; i < threads.Length; i++)
            {
                threads[i].Start(new object[] {i});
            }
            foreach (var thread in threads)
                thread.Join();
            sw.Stop();
            Console.WriteLine(string.Format("Проект выполнен за {0} (дней), а мог быть выполнен за {1} (дней).",
                sw.ElapsedMilliseconds / 1000, realTime / 1000));
            Console.ReadKey();
        }
    }
}
