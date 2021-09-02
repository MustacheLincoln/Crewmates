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

                Vector3 relativePos = target.position - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(relativePos);


                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 250))
                {
                    toRotation = Quaternion.LookRotation(-relativePos);
                }


                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * (speed / 10) * Time.deltaTime);
            }
        }
    }
}

