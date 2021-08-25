using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Crewmates
{
    public class Rum : Consumable, ITask, IReadiable
    {
        private float modifierDuration = 100;
        private bool ready = false;

        private void Update()
        {
            if (ready)
            {
                if (gm.globalTasks.Contains(gameObject) == false)
                {
                    if (!beingUsed)
                    {
                        if (!isStored)
                        {
                            if (gm.crates.Count > 0)
                            {
                                foreach (Crate crate in gm.crates)
                                {
                                    if (crate.items + crate.incomingItems < crate.maxItems)
                                        if (gm.globalTasks.Contains(gameObject) == false)
                                            gm.globalTasks.Add(gameObject);
                                }
                            }
                        }
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
                                transform.SetParent(crate.transform);
                                transform.position = CratePosition(crate);
                                storedIn = crate;
                                isStored = true; 
                                beingUsed = false;
                                crewmate.ClearTask();
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

        private Vector3 CratePosition(Crate crate)
        {
            switch (crate.items)
            {
                case 1:
                    return crate.transform.position + (Vector3.left + Vector3.forward) / 4;
                case 2:
                    return crate.transform.position + (Vector3.right + Vector3.forward) / 4;
                case 3:
                    return crate.transform.position + (Vector3.left + Vector3.back) / 4;
                case 4:
                    return crate.transform.position + (Vector3.right + Vector3.back) / 4;
            }
            return crate.transform.position;
        }

        public override void Consume(Crewmate crewmate)
        {
            if (storedIn)
                storedIn.items--;
            crewmate.drunkeness += modifierDuration;
            Destroy(gameObject);
        }

        public void Ready()
        {
            ready = true;
        }
    }
}
