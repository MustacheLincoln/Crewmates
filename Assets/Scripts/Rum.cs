using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Crewmates
{
    public class Rum : MonoBehaviour, ITask
    {
        public bool isStored;

        private GameManager gm;
        private bool beingUsed;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            if (!beingUsed)
            {
                if (gm.crates.Count > 0)
                {
                    foreach (Crate crate in gm.crates)
                    {
                        if (crate.items + crate.incomingItems < crate.maxItems)
                            if (!gm.globalTasks.Contains(gameObject))
                                gm.globalTasks.Add(gameObject);
                    }
                }
            }
        }

        public void Task(Crewmate crewmate)
        {
            beingUsed = true;
            List<Crate> openCrates = new List<Crate>(gm.crates);
            foreach (Crate crate in gm.crates)
            {
                if (crate.items + crate.incomingItems >= crate.maxItems)
                    openCrates.Remove(crate);
            }
            if (openCrates.Count > 0)
            {
                float closestDist = openCrates.Min(c => (c.transform.position - transform.position).magnitude);
                foreach (Crate crate in openCrates)
                {
                    float dist = (crate.transform.position - transform.position).magnitude;
                    if (dist == closestDist)
                    {
                        crate.incomingItems++;
                        crewmate.MoveTo(transform.position, () =>
                        {
                            transform.SetParent(crewmate.transform);
                            crewmate.MoveTo(crate.transform.position, () =>
                            {
                                crate.incomingItems--;
                                crate.items++;
                                Destroy(gameObject);
                            });
                        });
                    }

                }
            }
            else
            {
                crewmate.ClearTask();
                beingUsed = false;
            }

        }
    }
}
