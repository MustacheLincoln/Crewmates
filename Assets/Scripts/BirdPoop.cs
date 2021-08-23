using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class BirdPoop : MonoBehaviour, ITask
    {
        private GameManager gm;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
            gm.globalTasks.Add(gameObject);
        }

        public void Task(Crewmate crewmate)
        {
            crewmate.MoveTo(transform.position, () =>
            {
                Destroy(gameObject);
            });
        }
    }
}
