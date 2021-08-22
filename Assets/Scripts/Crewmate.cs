using System;
using System.Linq;
using UnityEngine;

namespace Crewmates
{
    public class Crewmate : MonoBehaviour
    {
        private GameManager gm;
        private CrewmateNavMesh navMesh;

        public GameObject myTask;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
            navMesh = GetComponent<CrewmateNavMesh>();
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
            if (gm.tasks.Count > 0)
            {
                float closestDist = gm.tasks.Min(t => (t.transform.position - transform.position).magnitude);
                foreach (GameObject task in gm.tasks)
                {
                    float dist = (task.transform.position - transform.position).magnitude;
                    if (dist == closestDist)
                    {
                        gm.tasks.Remove(task);
                        myTask = task;
                        task.GetComponent<ITaskable>().Task(this);
                        break;
                    }
                }
            }
            else
                MoveTo(Vector3.zero);
        }

        public void ClearTask()
        {
            myTask = null;
        }
    }
}

