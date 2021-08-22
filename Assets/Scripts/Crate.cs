using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Crate : MonoBehaviour
    {
        public int items = 0;
        public int incomingItems = 0;
        public int maxItems = 4;

        private GameManager gm;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
            gm.crates.Add(this);
        }

    }
}