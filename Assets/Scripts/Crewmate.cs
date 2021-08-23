using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Crewmates
{
    public class Crewmate : MonoBehaviour
    {
        private GameManager gm;
        private CrewmateNavMesh navMesh;

        public GameObject myTask;
        private Vector3 wanderPosition;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
            gm.crewmates.Add(this);
            navMesh = GetComponent<CrewmateNavMesh>();
            Invoke("ChangeWanderPosition", UnityEngine.Random.Range(2, 8));
        }

        private void Start()
        {
            //Change to match their Rank once implimented
            navMesh.ChangePriority(UnityEngine.Random.Range(0, 99));
        }

        public void MoveTo(Vector3 position, Action onArrivedAtPosition = null)
        {
            navMesh.MoveTo(position, onArrivedAtPosition);
        }

        private void Update()
        {
            if (myTask == null)
            {
                FindTask();
            }
        }

        private void FindTask()
        {
            if (gm.globalTasks.Count > 0)
            {
                float closestTask = gm.globalTasks.Min(t => (t.transform.position - transform.position).magnitude);
                foreach (GameObject task in gm.globalTasks.ToList())
                {
                    float dist = (task.transform.position - transform.position).magnitude;
                    if (dist == closestTask)
                    {
                        List<Crewmate> idleCrewmates = new List<Crewmate>(gm.crewmates);
                        foreach (Crewmate crewmate in gm.crewmates)
                        {
                            if (crewmate.myTask != null)
                                idleCrewmates.Remove(crewmate);
                        }
                        if (idleCrewmates.Count > 0)
                        {
                            float closestCrewmate = idleCrewmates.Min(c => (c.transform.position - task.transform.position).magnitude);
                            foreach (Crewmate crewmate in idleCrewmates)
                            {
                                if (dist == closestCrewmate)
                                {
                                    gm.globalTasks.Remove(task);
                                    myTask = task;
                                    task.GetComponent<ITask>().Task(this);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
                MoveTo(wanderPosition);
        }
        private void ChangeWanderPosition()
        {
            wanderPosition = gm.GetRandomPosition();
            Invoke("ChangeWanderPosition", UnityEngine.Random.Range(2, 8));
        }


        public void ClearTask()
        {
            myTask = null;
        }
    }
}

