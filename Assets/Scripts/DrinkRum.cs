using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Crewmates
{
    public class DrinkRum : MonoBehaviour, ITask
    {
        public void Task(Crewmate crewmate)
        {
            var rums = FindObjectsOfType<Rum>();
            if (rums.Length > 0)
            {
                foreach (Rum rum in rums)
                {   
                    float closestDist = rums.Min(r => (r.transform.position - crewmate.transform.position).magnitude);
                    float dist = (rum.transform.position - crewmate.transform.position).magnitude;
                    if (dist == closestDist)
                    {
                        rum.beingUsed = true;
                        rum.RemoveTask();
                        crewmate.MoveTo(rum.gameObject.transform.position, () =>
                        {
                            rum.Drank(crewmate);
                            Destroy(gameObject);
                        });
                    }
                }
            }
        }
    }
}

