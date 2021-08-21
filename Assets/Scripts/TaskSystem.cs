using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Crewmates
{
    public class TaskSystem
    {
        public abstract class Task
        {
            public class MoveToPosition : Task
            {
                public Vector3 movePosition;
            }

            public class BirdPoopCleanUp : Task
            {
                public Vector3 poopPosition;
                public Action cleanUpAction;
            }

            public class TakeRumToCrate : Task
            {
                public Vector3 rumPosition;
                public Action<CrewmateTaskAI> grabRum;
                public Vector3 cratePosition;
                public Action dropRum;
            }
        }

        public class QueuedTask
        {
            private Func<Task> tryGetTaskFunc;
            public QueuedTask(Func<Task> tryGetTaskFunk)
            {
                this.tryGetTaskFunc = tryGetTaskFunk;
            }

            public Task TryDequeueTask()
            {
                return tryGetTaskFunc();
            }
        }

        private List<Task> taskList;
        private List<QueuedTask> queuedTaskList;

        public TaskSystem()
        {
            taskList = new List<Task>();
            queuedTaskList = new List<QueuedTask>();
            FunctionPeriodic.Create(DequeueTasks, .2f);
        }
        public void AddTask(Task task)
        {
            taskList.Add(task);
        }

        public void EnqueueTask(QueuedTask queuedTask)
        {
            queuedTaskList.Add(queuedTask);
        }
        //Helper Function
        public void EnqueueTask(Func<Task> tryGetTaskFunc)
        {
            QueuedTask queuedTask = new QueuedTask(tryGetTaskFunc);
            queuedTaskList.Add(queuedTask);
        }

        private void DequeueTasks()
        {
            for (int i = 0; i < queuedTaskList.Count; i++)
            {
                QueuedTask queuedTask = queuedTaskList[i];
                Task task = queuedTask.TryDequeueTask();
                if (task != null)
                {
                    // Task Dequeued, add to normal list
                    AddTask(task);
                    queuedTaskList.RemoveAt(i);
                    i--;
                }
            }
        }

        public Task RequestNextTask()
        {
            if (taskList.Count > 0)
            {
                Task task = taskList[0];
                taskList.RemoveAt(0);
                return task;
            }
            else
            {
                return null;
            }
        }


    }
}
