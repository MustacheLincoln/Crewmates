using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Crewmates
{
    public class GameHandler : MonoBehaviour
    {
        public GameObject crewmate;
        public TaskSystem taskSystem;
        [SerializeField] private GameObject birdPoopGO;
        [SerializeField] private GameObject rumGO;
        [SerializeField] private GameObject crateGO;

        private List<Crate> crates;

        void Awake()
        {
            taskSystem = new TaskSystem();

            Instantiate(crewmate, new Vector3(-1.5f, 1.5f, 7), Quaternion.identity);
            Instantiate(crewmate, new Vector3(-1.5f, 1.5f, 6), Quaternion.identity);
            /*Redundant
            CrewmateTaskAI crewmateTaskAI = crewmate.gameObject.GetComponent<CrewmateTaskAI>();
            Crewmate cm = crewmate.gameObject.GetComponent<Crewmate>();
            crewmateTaskAI.Setup(cm, taskSystem);
            */

            crates = new List<Crate>();

            SpawnBirdPoop(new Vector3(0,.51f,0));
            SpawnBirdPoop(new Vector3(1.5f, .51f, -7));
            SpawnRum(new Vector3(0, .78f, 7));
            SpawnRum(new Vector3(1.5f, .78f, 0));
            SpawnCrate(new Vector3(-3.3f, 1, -8.3f));
        }

        private void SpawnCrate(Vector3 position)
        {
            GameObject crate = Instantiate(crateGO, position, Quaternion.identity);
            crates.Add(crate.GetComponent<Crate>());
        }

        private void SpawnRum(Vector3 position)
        {
            GameObject rum = Instantiate(rumGO, position, Quaternion.Euler(-90, 0, 0));
            taskSystem.EnqueueTask(() =>
            {
                foreach (Crate crate in crates)
                {
                    if (crate.items + crate.incomingItems < crate.maxItems)
                    {
                        crate.incomingItems++;
                        //crate.SetHasItemIncoming(true);
                        TaskSystem.Task task = new TaskSystem.Task.TakeRumToCrate
                        {
                            rumPosition = position,
                            cratePosition = crate.GetPosition(),
                            grabRum = (CrewmateTaskAI crewmateTaskAI) =>
                            {
                                rum.transform.SetParent(crewmateTaskAI.transform);
                            },
                            dropRum = () =>
                            {
                                rum.transform.SetParent(null);
                                crate.incomingItems--;
                                crate.items++;
                            }
                        };
                        return task;
                    }   
                }
                return null;
            });
        }

        private void SpawnBirdPoop(Vector3 position)
        {
            GameObject birdPoop = Instantiate(birdPoopGO, position, Quaternion.Euler(90,0,0));
            TaskSystem.Task task = new TaskSystem.Task.BirdPoopCleanUp 
            {
                poopPosition = position,
                cleanUpAction = () =>
                {
                    Destroy(birdPoop);
                }             
            };
            taskSystem.AddTask(task);
        }
    }
}

