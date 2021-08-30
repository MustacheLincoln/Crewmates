using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class ShipMovement : MonoBehaviour
    {
        public Transform worldMove;
        public Transform worldRotate;
        public Transform target;

        public bool docked;
        public bool anchored;

        private float turnSpeed = .5f;
        private float speed;
        private float topSpeed = 10;

        private void Awake()
        {
            docked = false;
        }

        private void Update()
        {
            if (!docked && !anchored)
            {
                if (speed < topSpeed)
                    speed += 1 * Time.deltaTime;
                worldMove.position -= transform.forward * speed * Time.deltaTime;
            }
        }
    }
}

