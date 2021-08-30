using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class EnemyShipMovement : MonoBehaviour
    {
        public Transform target;
        private float turnSpeed = .5f;
        private float speed;
        private float topSpeed = 10;

        private void Update()
        {
            if (speed < topSpeed)
                speed += 1 * Time.deltaTime;
            transform.position += transform.forward * speed * Time.deltaTime;

            Vector3 relativePos = target.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * (speed/10) * Time.deltaTime);
        }
    }
}

