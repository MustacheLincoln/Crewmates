using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Crewmates
{
    public class DrinkWater : MonoBehaviour, ITask
    {
        private GameManager gm;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
        }

        public void Task(Crewmate crewmate)
        {
            Water water = ClosestWater(crewmate);
            if (water)
            {
                water.beingUsed = true;
                water.RemoveTask();
                crewmate.MoveTo(water.gameObject.transform.position, () =>
                {
                    water.Drank(crewmate);
                    Destroy(gameObject);
                });
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private Water ClosestWater(Crewmate crewmate)
        {
            Water closestWater = null;
            float closestDist = Mathf.Infinity;
            foreach (Water water in FindObjectsOfType<Water>().ToList())
            {
                if (water.beingUsed == false)
                {
                    float dist = (water.transform.position - transform.position).magnitude;
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closestWater = water;
                    }
                }

            }
            return closestWater;
        }
    }
}

