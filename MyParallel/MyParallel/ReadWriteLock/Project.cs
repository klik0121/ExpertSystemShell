using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MyParallel.ReadWriteLock
{
    public class Project
    {
        protected Queue<string> toDoList;
        protected ReaderWriterLockSlim taskLock;
        protected int tasksCompleted;
        protected int tasksGiven;
        protected bool deadLine;

        public Queue<string> ToDoList
        {
            get { return toDoList; }
        }

        public Project()
        {
            toDoList = new Queue<string>();
            taskLock = new ReaderWriterLockSlim();
            deadLine = false;
            tasksCompleted = 0;
            tasksGiven = 0;
        }
        
        public bool GetCurrentTask(out string task)
        {
            bool result = false;
            try
            {
                task = null;
                taskLock.EnterWriteLock();
                if (toDoList.Count > 0)
                {
                    task = toDoList.Dequeue();
                    result = true;
                }
            }
            finally
            {
                taskLock.ExitWriteLock();
            }
            return result;
        }
        public void CompleteTask()
        {
            taskLock.EnterWriteLock();
            try
            {
                tasksCompleted++;
            }
            finally
            {
                taskLock.ExitWriteLock();
            }
        }
        public void AppendTask(string task)
        {
            taskLock.EnterWriteLock();
            try
            {
                toDoList.Enqueue(task);
                tasksGiven++;
            }
            finally
            {
                taskLock.ExitWriteLock();
            }
        }
        public bool IsFinished()
        {
            return deadLine && (tasksCompleted == tasksGiven);
        }
        public bool TimeToWork()
        {
            taskLock.EnterReadLock();
            bool result = false;
            try
            {
                result |= (tasksGiven - tasksCompleted) > 5;
                result |= deadLine;
            }
            finally
            {
                taskLock.ExitReadLock();
            }
            return result;
        }
        public void SetDeadLine()
        {
            taskLock.EnterWriteLock();
            try
            {
                deadLine = true;
            }
            finally
            {
                taskLock.ExitWriteLock();
            }
        }
    }
}
