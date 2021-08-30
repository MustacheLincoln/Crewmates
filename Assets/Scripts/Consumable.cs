using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public abstract class Consumable : MonoBehaviour
    {
        public bool isStored;
        public Storage storedIn;
        public bool beingUsed = true;

        private void Awake()
        {
            beingUsed = true;
        }
        public void RemoveTask(GameObject consumable)
        {
            GameManager.Instance.globalTasks.Remove(consumable);
        }

        public abstract void Consume(Crewmate crewmate);
    }
}

