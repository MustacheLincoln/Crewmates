using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

namespace Crewmates
{
    public class Crewmate : MonoBehaviour, IReadiable
    {
        private GameManager gm;
        private CrewmateNavMesh navMesh;

        public GameObject myTask;
        public TMP_Text nameText;
        private Vector3 wanderPosition;
        private bool ready = false;
        private bool mousedOver;

        private float baseMood = 50;
        public float hydration = 100;
        public float drunkeness = 0;

        public bool thirsty = false;
        public bool drunk = false;
        public float moodModifiers = 0;
        public float mood;


        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
            navMesh = GetComponent<CrewmateNavMesh>();
            ChangeWanderPosition();
        }

        private void Start()
        {
            //Change to match their Rank once implimented
            navMesh.ChangePriority(UnityEngine.Random.Range(0, 99));
            name = gm.GenerateName();
            nameText.text = name;
            nameText.enabled = false;
        }

        public void MoveTo(Vector3 position, Action onArrivedAtPosition = null)
        {
            navMesh.MoveTo(position, onArrivedAtPosition);
        }

        private void Update()
        {
            if (ready)
            {
                nameText.enabled = (mousedOver == true || gm.selected == this.gameObject);
                Mood();
                if (myTask == null)
                    if (hydration < 20)
                        GetConsumable(FindObjectsOfType<Water>());
                if (myTask == null)
                    if (mood < 50)
                        GetConsumable(FindObjectsOfType<Rum>());
                if (myTask == null)
                    FindGlobalTask();
            }
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
            GameObject closestTask = null;
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

        public void Mood()
        {
            hydration -= Time.deltaTime;
            hydration = Mathf.Clamp(hydration, 0, Mathf.Infinity);
            int thirstyModifier = 10;
            if (hydration <= 0)
            {
                if (thirsty == false)
                {
                    thirsty = true;
                    moodModifiers -= thirstyModifier;
                }
            }
            else
            {
                if (thirsty == true)
                {
                    thirsty = false;
                    moodModifiers += thirstyModifier;
                }
            }

            drunkeness -= Time.deltaTime;
            drunkeness = Mathf.Clamp(drunkeness, 0, Mathf.Infinity);
            int drunkModifier = 10;
            if (drunkeness > 0)
            {
                if (drunk == false)
                {
                    drunk = true;
                    moodModifiers += drunkModifier;
                    navMesh.speed = navMesh.speed / 2;
                }
            }
            else
            {
                if (drunk == true)
                {
                    drunk = false;
                    moodModifiers -= drunkModifier;
                    navMesh.speed = navMesh.speed * 2;
                }
            }

            mood = baseMood + moodModifiers;
        }

        public void Ready()
        {
            ready = true;
            transform.Find("Mesh").position += Vector3.down;
            navMesh.Enable();
            gm.crewmates.Add(this);
        }

        internal void MouseEnter()
        {
            mousedOver = true;
        }

        internal void MouseExit()
        {
            mousedOver = false;
        }
    }
}

