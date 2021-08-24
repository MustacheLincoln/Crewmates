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
            thirst -= Time.deltaTime;
            if (myTask == null)
                if (thirst < 50)
                    GetConsumable(FindObjectsOfType<Water>());
            mood -= Time.deltaTime;
            if (myTask == null)
                if (mood < 50)
                    GetConsumable(FindObjectsOfType<Rum>());
            if (myTask == null)
                FindGlobalTask();

        }

        public void GetConsumable(Array array)
        {
            Consumable consumable = ClosestConsumable(array);
            if (consumable)
            {
                consumable.beingUsed = true;
                consumable.RemoveTask(consumable.gameObject);
                myTask = consumable.gameObject;
                MoveTo(consumable.gameObject.transform.position, () =>
                {
                    consumable.Consume(this);
                    Destroy(consumable.gameObject);
                });
            }
        }

        private Consumable ClosestConsumable(Array array)
        {
            Consumable closestConsumable = null;
            float closestDist = Mathf.Infinity;
            foreach (Consumable c in array)
            {
                if (c.beingUsed == false)
                {
                    float dist = (c.transform.position - transform.position).magnitude;
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closestConsumable = c;
                    }
                }

            }
            return closestConsumable;
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

