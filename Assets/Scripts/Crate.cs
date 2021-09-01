using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Crate : Storage, IReadiable
    {
        private void Awake()
        {
            items = 0;
            incomingItems = 0;
            maxItems = 4;
        }

        public void Ready()
        {
            GameManager.Instance.crates.Add(this);
            transform.Find("Mesh").gameObject.layer = 0; //TEMP
        }

    }
}