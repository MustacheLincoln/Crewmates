using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Crate : Storage, IReadiable
    {
        private GameManager gm;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
            items = 0;
            incomingItems = 0;
            maxItems = 4;
        }

        public void Ready()
        {
            gm.crates.Add(this);
        }

    }
}