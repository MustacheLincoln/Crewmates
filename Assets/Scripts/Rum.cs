using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Rum : Consumable, ITask, IReadiable
    {
        private void Update()
        {
            if (GameManager.Instance.globalTasks.Contains(gameObject) == false)
            {
                if (!beingUsed)
                {
                    if (!isStored)
                    {
                        if (GameManager.Instance.crates.Count > 0)
                        {
                            foreach (Crate crate in GameManager.Instance.crates)
                            {
                                if (crate.items + crate.incomingItems < crate.maxItems)
                                    if (GameManager.Instance.globalTasks.Contains(gameObject) == false)
                                        GameManager.Instance.globalTasks.Add(gameObject);
                            }
                        }
                    }
                }
            }
        }

        public void Task(Crewmate crewmate)
        {
            beingUsed = true;
            List<Crate> openCrates = new List<Crate>(GameManager.Instance.crates);
            foreach (Crate crate in GameManager.Instance.crates)
            {
                if (crate.items + crate.incomingItems >= crate.maxItems)
                    openCrates.Remove(crate);
            }
            if (openCrates.Count > 0)
            {
                Crate closestCrate = ClosestCrate(openCrates);
                closestCrate.incomingItems++;
                crewmate.MoveTo(transform.position, () =>
                {
                    transform.SetParent(crewmate.rightHand);
                    transform.position = crewmate.rightHand.position;
                    transform.rotation = crewmate.rightHand.rotation * Quaternion.Euler(90, 0, 0);
                    PickedUp();
                    crewmate.MoveTo(closestCrate.transform.position, () =>
                    {
                        closestCrate.incomingItems--;
                        closestCrate.items++;
                        transform.SetParent(closestCrate.transform);
                        transform.position = CratePosition(closestCrate);
                        transform.rotation = Quaternion.identity;
                        SetDown();
                        storedIn = closestCrate;
                        isStored = true; 
                        beingUsed = false;
                        crewmate.ClearTask();
                    });
                });
            }
            else
            {
                crewmate.ClearTask();
                beingUsed = false;
            }

        }

        private Crate ClosestCrate(List<Crate> openCrates)
        {
            Crate closestCrate = null;
            float closestDist = Mathf.Infinity;
            foreach (Crate crate in openCrates)
            {
                float dist = (crate.transform.position - transform.position).magnitude;
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestCrate = crate;
                }
            }
            return closestCrate;
        }

        public void PickedUp()
        {
            transform.Find("Mesh").gameObject.SetActive(false);
            transform.Find("CarriedMesh").gameObject.SetActive(true);
        }

        public void SetDown()
        {
            transform.Find("Mesh").gameObject.SetActive(true);
            transform.Find("CarriedMesh").gameObject.SetActive(false);
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
            crewmate.drunk.Begin();
            Destroy(gameObject);
        }

        public void Ready()
        {
            beingUsed = false;
        }
    }
}
