using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Crate : MonoBehaviour
    {
        public int items;
        public int incomingItems;
        public int maxItems;

        private void Awake()
        {
            items = 0;
            incomingItems = 0;
            maxItems = 4;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}