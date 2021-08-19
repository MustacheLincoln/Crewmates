using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crewmates
{
    public class CrewmateNavMesh : MonoBehaviour
    {
        public Vector3 movementTarget;

        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            navMeshAgent.destination = movementTarget;
        }
    }
}

