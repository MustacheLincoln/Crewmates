using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Crate : Storage
    {
        private GameManager gm;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
            gm.crates.Add(this);
            items = 0;
            incomingItems = 0;
            maxItems = 4;
        }

    }
}