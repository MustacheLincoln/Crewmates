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
        public GameManager gameManager;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            beingUsed = true;
        }
        public void RemoveTask(GameObject consumable)
        {
            gameManager.globalTasks.Remove(consumable);
        }

        public abstract void Consume(Crewmate crewmate);
    }
}

