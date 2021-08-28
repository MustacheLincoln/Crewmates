using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class BoatMovement : MonoBehaviour
    {
        private float speed = 1;
        void Update()
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
    }
}

