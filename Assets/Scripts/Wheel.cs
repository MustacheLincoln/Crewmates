using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Wheel : MonoBehaviour, ITask, IReadiable
    {
        public Crewmate stationed;

        private void Start()
        {
            Ready();//?
        }

        public void Ready()
        {
            if (!GameManager.Instance.globalTasks.Contains(this.gameObject))
                GameManager.Instance.globalTasks.Add(this.gameObject);
        }

        public void Task(Crewmate crewmate)
        {
            crewmate.MoveTo(transform.position, () =>
            {
                stationed = crewmate;
            });
        }

        private void Update()
        {
            if (stationed)
            {
                GameManager.Instance.playerShip.GetComponent<ShipMovement>().anchored = false;
                GameManager.Instance.playerShip.GetComponent<ShipMovement>().docked = false;
            }
        }
    }
}
