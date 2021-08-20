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
        [SerializeField] private GameObject birdPoopSprite;

        void Awake()
        {
            taskSystem = new TaskSystem();

            Instantiate(crewmate, new Vector3(-1.5f, 1.5f, 7), Quaternion.identity);
            /*Redundant
            CrewmateTaskAI crewmateTaskAI = crewmate.gameObject.GetComponent<CrewmateTaskAI>();
            Crewmate cm = crewmate.gameObject.GetComponent<Crewmate>();
            crewmateTaskAI.Setup(cm, taskSystem);
            */

            SpawnBirdPoop(new Vector3(0,.50f,0));
            SpawnBirdPoop(new Vector3(1.5f, .50f, -7));
        }

        private void SpawnBirdPoop(Vector3 position)
        {
            GameObject birdPoop = Instantiate(birdPoopSprite, position, Quaternion.Euler(90,0,0));
            SpriteRenderer sprite = birdPoop.GetComponent<SpriteRenderer>();
            TaskSystem.Task task = new TaskSystem.Task.BirdPoopCleanUp {
                targetPosition = position,
                cleanUpAction = () =>
                {
                    float alpha = 1;
                    FunctionUpdater.Create(() =>
                    {
                        alpha -= Time.deltaTime;
                        sprite.color = new Color(1, 1, 1, alpha);
                        if (alpha <= 0)
                        {
                            Destroy(birdPoop);
                            return true;
                        }
                        else
                            return false;
                    });
                }
                
            };
            //taskSystem.AddTask(task);
            float cleanUpTime = Time.time + 5;
            taskSystem.EnqueueTask(() =>
            {
                if (Time.time >= cleanUpTime)
                {
                    return task;
                }
                else
                    return null;
            });
        }
    }
}

