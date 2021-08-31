using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class ShipMovement : MonoBehaviour
    {

        public Transform target;

        public bool docked;
        public bool anchored;

        private float turnSpeed = .1f;
        private float speed;
        private float topSpeed = 10;

        private void Awake()
        {
            docked = false;
        }

        private void Update()
        {
            if (!docked && !anchored && target)
            {
                if (speed < topSpeed)
                    speed += 1 * Time.deltaTime;
                transform.position += transform.forward * speed * Time.deltaTime;

                Vector3 relativePos = (target.position + Vector3.right * 25) - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(relativePos);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * (speed / 10) * Time.deltaTime);
            }
        }
    }
}

