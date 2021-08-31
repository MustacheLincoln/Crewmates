using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crewmates
{
    public class CrewmateNavMesh : MonoBehaviour
    {
        public bool isMoving = false;
        private float baseSpeed = 3.5f;
        public float speed;
        public float velocity;
        private Action onArrivedAtPosition;
        private NavMeshAgent navMeshAgent;

        //TEMP
        public Transform destination;

        private void Awake()
        {
            speed = baseSpeed;
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;
        }
        internal void MoveTo(Vector3 position, Action onArrivedAtPosition)
        {
            //navMeshAgent.destination = position;
            this.onArrivedAtPosition = onArrivedAtPosition;
            isMoving = true;
        }

        private void Update()
        {
            //velocity = navMeshAgent.velocity.magnitude;
            //navMeshAgent.speed = speed;
            if (destination) //TEMP
            {
                if (Vector3.Distance(transform.position, destination.position) <= 1.25)//(Vector3.Distance(transform.position, navMeshAgent.destination) <= 1.25)
                {
                    if (isMoving == true)
                    {
                        onArrivedAtPosition?.Invoke();
                        isMoving = false;
                    }
                }
                else
                    if (isMoving == false)
                        isMoving = true;
            }
        }

        internal void ChangePriority(int priority)
        {
            //navMeshAgent.avoidancePriority = priority;
        }

        public void Enable()
        {
            //navMeshAgent.enabled = true;
        }

    }
}

