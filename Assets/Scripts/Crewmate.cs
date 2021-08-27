using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

namespace Crewmates
{
    public class Crewmate : MonoBehaviour, IReadiable
    {
        private GameManager gameManager;
        private CrewmateNavMesh navMesh;
        private Animator animator;
        public Transform rightHand;

        public GameObject myTask;
        public TMP_Text nameText;
        private Vector3 wanderPosition;
        private bool ready = false;
        private bool mousedOver;

        private float baseMood = 50;
        public float hydration = 100;

        public float modifiers = 0;
        public float mood;
        public List<string> modifierDescriptions;

        public Drunk drunk;
        public Thirsty thirsty;

        private void Awake()
        {
            drunk = gameObject.AddComponent<Drunk>();
            thirsty = gameObject.AddComponent<Thirsty>();
            gameManager = FindObjectOfType<GameManager>();
            navMesh = GetComponent<CrewmateNavMesh>();
            animator = GetComponentInChildren<Animator>();
            ChangeWanderPosition();
        }

        private void Start()
        {
            //Change to match their Rank once implimented
            navMesh.ChangePriority(UnityEngine.Random.Range(0, 99));
            name = gameManager.GenerateName();
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
                nameText.enabled = (mousedOver == true || gameManager.selected == this.gameObject);
                Mood();
                if (myTask == null)
                    if (hydration < 20)
                        GetConsumable(FindObjectsOfType<Water>());
                if (myTask == null)
                    if (mood < 50)
                        GetConsumable(FindObjectsOfType<Rum>());
                if (myTask == null)
                    FindGlobalTask();

                animator.SetBool("Run", (navMesh.velocity > .5f));
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
                    animator.SetTrigger("Consume");
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
            if (gameManager.globalTasks.Count > 0)
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
            gameManager.globalTasks.Remove(myTask);
            myTask.GetComponent<ITask>().Task(this);
        }

        private bool IsClosestCrewmate(GameObject obj)
        {
            Crewmate closestCrewmate = null;
            float closestDist = Mathf.Infinity;
            foreach (Crewmate crewmate in gameManager.crewmates)
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
            foreach (GameObject task in gameManager.globalTasks.ToList())
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
            wanderPosition = gameManager.GetRandomPosition();
            Invoke("ChangeWanderPosition", UnityEngine.Random.Range(6, 10));
        }


        public void ClearTask()
        {
            myTask = null;
        }

        public void Mood()
        {
            hydration -= Time.deltaTime;
            hydration = Mathf.Clamp(hydration, 0, Mathf.Infinity);
            if (hydration <= 0)
                thirsty.Begin();
            else
                thirsty.End();

            mood = baseMood + modifiers;
        }

        public void Ready()
        {
            ready = true;
            navMesh.Enable();
            gameManager.crewmates.Add(this);
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

