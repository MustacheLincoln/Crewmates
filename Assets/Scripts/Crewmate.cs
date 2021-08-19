using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Crewmate : MonoBehaviour, IMoveable
    {
        private CrewmateNavMesh navMesh;

        private void Start()
        {
            navMesh = GetComponent<CrewmateNavMesh>();
        }

        public void MoveTo(Vector3 position, Action onArrivedAtPosition = null)
        {
            navMesh.movementTarget = position;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}

