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

        private GameHandler gameHandler;
        private IMoveable crewmate;
        private TaskSystem taskSystem;
        [SerializeField] private State state;
        private float awareness = 1;
        private float waitingTimer;

        private void Awake()
        {
            //My own stuff not from the tutorial
            gameHandler = FindObjectOfType<GameHandler>();
            crewmate = GetComponent<IMoveable>();
            //
            taskSystem = gameHandler.taskSystem;
        }

        public void Setup(IMoveable crewmate, TaskSystem taskSystem)
        {
            //Redundant
            this.crewmate = crewmate;
            this.taskSystem = taskSystem;

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
            TaskSystem.Task task = taskSystem.RequestNextTask();
            if (task == null)
            {
                state = State.WaitingForNextTask;
            }
            else
            {
                state = State.ExecutingTask;
                if (task is TaskSystem.Task.MoveToPosition)
                {
                    ExecuteMoveToPositionTask(task as TaskSystem.Task.MoveToPosition);
                    return;
                }
                if (task is TaskSystem.Task.BirdPoopCleanUp)
                {
                    ExecuteBirdPoopCleanUpTask(task as TaskSystem.Task.BirdPoopCleanUp);
                    return;
                }
                Debug.LogError("Task type unknown!");
            }
        }

        private void ExecuteMoveToPositionTask(TaskSystem.Task.MoveToPosition task)
        {
            crewmate.MoveTo(task.targetPosition, () =>
            {
                state = State.WaitingForNextTask;
            });
        }

        private void ExecuteBirdPoopCleanUpTask(TaskSystem.Task.BirdPoopCleanUp task)
        {
            crewmate.MoveTo(task.targetPosition, () => {
                task.cleanUpAction();
                state = State.WaitingForNextTask;
            });
        }
    }
}

