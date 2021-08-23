using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Crewmates
{
    public class DrinkRum : MonoBehaviour, ITask
    {
        private GameManager gm;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
        }

        public void Task(Crewmate crewmate)
        {
            Rum rum = ClosestRum(crewmate);
            if (rum)
            {
                rum.beingUsed = true;
                rum.RemoveTask();
                crewmate.MoveTo(rum.gameObject.transform.position, () =>
                {
                    rum.Drank(crewmate);
                    Destroy(gameObject);
                });
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private Rum ClosestRum(Crewmate crewmate)
        {
            Rum closestRum = null;
            float closestDist = Mathf.Infinity;
            foreach (Rum rum in FindObjectsOfType<Rum>().ToList())
            {
                if (rum.beingUsed == false)
                {
                    float dist = (rum.transform.position - transform.position).magnitude;
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closestRum = rum;
                    }
                }

            }
            return closestRum;
        }
    }
}

