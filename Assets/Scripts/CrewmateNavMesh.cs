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
        private Action onArrivedAtPosition;
        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        internal void MoveTo(Vector3 position, Action onArrivedAtPosition)
        {
            navMeshAgent.destination = position;
            this.onArrivedAtPosition = onArrivedAtPosition;
            isMoving = true;
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, navMeshAgent.destination) <= 1)
            {
                if(isMoving == true)
                {
                    onArrivedAtPosition?.Invoke();
                    isMoving = false;
                }
            }
            else
                if (isMoving == false)
                    isMoving = true;
        }

        internal void ChangePriority(int priority)
        {
            navMeshAgent.avoidancePriority = priority;
        }
    }
}

