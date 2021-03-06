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

        private float turnSpeed = .15f;
        public float speed;
        private float topSpeed = 10;

        private void Start()
        {
            if (gameObject.CompareTag("Player"))
            {
                docked = true;
                anchored = true;
            }
        }


        private void Update()
        {
            if (!docked && !anchored)
            {
                if (speed < topSpeed)
                    speed += 1 * Time.deltaTime;
                transform.position += transform.forward * speed * Time.deltaTime;

                if (target)
                {
                    Vector3 relativePos = target.position - transform.position;
                    Quaternion toRotation = Quaternion.LookRotation(relativePos);


                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, transform.right, out hit, 25))
                    {
                        toRotation = Quaternion.LookRotation(-transform.right);
                    }
                    if (Physics.Raycast(transform.position + transform.right * 10, transform.forward, out hit, 100))
                    {
                        toRotation = Quaternion.LookRotation(-transform.right);
                        if (speed > 4)
                            speed -= 2 * Time.deltaTime;
                    }
                    if (Physics.Raycast(transform.position, transform.forward + transform.right, out hit, 50))
                    {
                        toRotation = Quaternion.LookRotation(-transform.right);
                    }
                    if (Physics.Raycast(transform.position, -transform.right, out hit, 25))
                    {
                        toRotation = Quaternion.LookRotation(transform.right);
                    }
                    if (Physics.Raycast(transform.position + -transform.right * 10, transform.forward, out hit, 100))
                    {
                        toRotation = Quaternion.LookRotation(transform.right);
                        if (speed > 4)
                            speed -= 2 * Time.deltaTime;
                    }
                    if (Physics.Raycast(transform.position, transform.forward - transform.right, out hit, 50))
                    {
                        toRotation = Quaternion.LookRotation(transform.right);
                    }
                    if (Physics.Raycast(transform.position, transform.forward + transform.right, out hit, 30))
                    {
                        if (speed > 1)
                            speed -= 3 * Time.deltaTime;
                    }
                    if (Physics.Raycast(transform.position, transform.forward - transform.right, out hit, 30))
                    {
                        if (speed > 1)
                            speed -= 3 * Time.deltaTime;
                    }
                    if (Physics.Raycast(transform.position, transform.forward, out hit, 50))
                    {
                        if (speed > 1)
                            speed -= 3 * Time.deltaTime;
                    }


                    transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
                }

                
            }
        }
    }
}

