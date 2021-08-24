using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public abstract class Consumable : MonoBehaviour
    {
        public bool isStored;
        public Crate storedIn;
        public bool beingUsed;
        public GameManager gm;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
        }
        public void RemoveTask(GameObject consumable)
        {
            gm.globalTasks.Remove(consumable);
        }

        public abstract void Consume(Crewmate crewmate);
    }
}

