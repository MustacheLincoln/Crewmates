using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class EnemyShipMovement : MonoBehaviour
    {
        public Transform target;
        private float turnSpeed = .5f;
        private float speed = 10;

        private void Update()
        {
            transform.position += transform.forward * speed * Time.deltaTime;

            Vector3 relativePos = target.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
    }
}

