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

        public float mood = 100;
        public float thirst = 100;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
            gm.crewmates.Add(this);
            navMesh = GetComponent<CrewmateNavMesh>();
            ChangeWanderPosition();
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
            mood -= Time.deltaTime;
            if (myTask == null)
                if (mood < 50)
                    GetRum();
            if (myTask == null)
                FindGlobalTask();

        }

        public void GetRum()
        {
            Rum rum = ClosestRum();
            if (rum)
            {
                rum.beingUsed = true;
                rum.RemoveTask();
                myTask = rum.gameObject;
                MoveTo(rum.gameObject.transform.position, () =>
                {
                    rum.Drank(this);
                    Destroy(rum.gameObject);
                });
            }
        }

        private Rum ClosestRum()
        {
            Rum closestRum = null;
            float closestDist = Mathf.Infinity;
            foreach (Rum rum in FindObjectsOfType<Rum>())
            {
                if (rum.beingUsed == false)
                {
                    float dist = (rum.transform.position - transform.position).magnitude;
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closestRum = rum;
                    }
                }

            }
            return closestRum;
        }

        private void FindGlobalTask()
        {
            GameObject task;
            if (gm.globalTasks.Count > 0)
            {
                task = ClosestTask();
                if (IsClosestCrewmate(task))
                {
                    SetTask(task);
                }
            }
            else
                MoveTo(wanderPosition);
        }

        private void SetTask(GameObject task)
        {
            myTask = task;
            gm.globalTasks.Remove(myTask);
            myTask.GetComponent<ITask>().Task(this);
        }

        private bool IsClosestCrewmate(GameObject obj)
        {
            Crewmate closestCrewmate = null;
            float closestDist = Mathf.Infinity;
            foreach (Crewmate crewmate in gm.crewmates)
            {
                if (crewmate.myTask == null)
                {
                    float dist = (obj.transform.position - crewmate.transform.position).magnitude;
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closestCrewmate = crewmate;
                    }
                }
            }
            return (this == closestCrewmate);
        }

        private GameObject ClosestTask()
        {
            GameObject closestTask= null;
            float closestDist = Mathf.Infinity;
            foreach (GameObject task in gm.globalTasks.ToList())
            {
                float dist = (task.transform.position - transform.position).magnitude;
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestTask = task;
                }
            }
            return closestTask;
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

