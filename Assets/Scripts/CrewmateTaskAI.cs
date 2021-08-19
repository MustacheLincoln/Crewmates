using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class CrewmateTaskAI : MonoBehaviour
    {
        private enum State
        {
            WaitingForNextTask,
            ExecutingTask,
        }

        private IMoveable crewmate;
        private TaskSystem taskSystem;
        private State state;
        private float awareness = .2f;
        private float waitingTimer;

        public void Setup(IMoveable crewmate, TaskSystem taskSystem)
        {
            this.crewmate = crewmate;
            this.taskSystem = taskSystem;
            Debug.Log(crewmate);
            Debug.Log(taskSystem);
            state = State.WaitingForNextTask;
            waitingTimer = awareness;
        }

        private void Update()
        {
            switch (state)
            {
                case State.WaitingForNextTask:
                    waitingTimer -= Time.deltaTime;
                    if (waitingTimer <= 0)
                    {
                        waitingTimer = awareness;
                        RequestNextTask();
                    }
                    break;
            }
        }

        private void RequestNextTask()
        {
            if (taskSystem != null)
            {
                TaskSystem.Task task = taskSystem.RequestNextTask();
                if (task == null)
                {
                    state = State.WaitingForNextTask;
                }
                else
                {
                    state = State.ExecutingTask;
                    ExecuteTask(task);
                }
            }
        }

        private void ExecuteTask(TaskSystem.Task task)
        {
            Debug.Log("ExecuteTask");
            crewmate.MoveTo(task.targetPosition, () =>
            {
                state = State.WaitingForNextTask;
            });
        }
    }
}

