using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Crewmates
{
    public class Water : MonoBehaviour, ITask
    {
        public bool isStored;
        public bool beingUsed;

        private GameManager gm;
        private Crate storedIn;
        private float moodBoost = 25;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
        }

        private void Update()
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
                            transform.SetParent(crewmate.transform, true);
                            crewmate.MoveTo(crate.transform.position, () =>
                            {
                                crate.incomingItems--;
                                crate.items++;
                                transform.SetParent(crate.transform,true);
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

        public void RemoveTask()
        {
            gm.globalTasks.Remove(gameObject);
        }

        public void Drank(Crewmate crewmate)
        {
            if (storedIn)
                storedIn.items--;
            crewmate.mood += moodBoost;
            crewmate.seekingRum = false;
            Destroy(gameObject);
        }
    }
}
