using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Crate : Storage, IReadiable
    {
        private GameManager gameManager;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            items = 0;
            incomingItems = 0;
            maxItems = 4;
        }

        public void Ready()
        {
            gameManager.crates.Add(this);
            transform.Find("Mesh").gameObject.layer = 0; //TEMP
        }

    }
}