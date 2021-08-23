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
        [SerializeField] private GameObject drinkRumPrefab;
        [SerializeField] private GameObject drinkWaterPrefab;

        public GameObject myTask;
        private Vector3 wanderPosition;
        public List<GameObject> personalTasks;

        public float mood = 100;
        public float thirst = 100;
        public bool seekingRum = false;
        public bool seekingWater = false;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
            gm.crewmates.Add(this);
            navMesh = GetComponent<CrewmateNavMesh>();
            personalTasks = new List<GameObject>();
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
            /*
            thirst -= Time.deltaTime;
            if (thirst < 50)
            {
                if (seekingWater == false)
                {
                    seekingWater = true;
                    GameObject drinkWater = Instantiate(drinkWaterPrefab);
                    personalTasks.Add(drinkWater);
                }
            }
            */
            mood -= Time.deltaTime;
            if (mood < 50)
            {
                if (seekingRum == false)
                {
                    GetRum();
                }
            }
            if (myTask == null)
            {
                FindTask();
            }
        }

        public void GetRum()
        {
            seekingRum = true;
            GameObject drinkRum = Instantiate(drinkRumPrefab);
            personalTasks.Add(drinkRum);
        }

        private void FindTask()
        {
            GameObject task;
            if (personalTasks.Count > 0)
            {
                task = personalTasks[0];
                SetTask(task);
                return;
            }
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
            personalTasks.Remove(myTask);
            gm.globalTasks.Remove(myTask);
            myTask.GetComponent<ITask>().Task(this);
        }

        private bool IsClosestCrewmate(GameObject closestTask)
        {
            Crewmate closestCrewmate = null;
            float closestDist = Mathf.Infinity;
            foreach (Crewmate crewmate in gm.crewmates)
            {
                if (crewmate.myTask == null)
                {
                    float dist = (closestTask.transform.position - crewmate.transform.position).magnitude;
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

