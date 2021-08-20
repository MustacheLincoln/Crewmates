using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class GameHandler : MonoBehaviour
    {
        public GameObject crewmate;
        public TaskSystem taskSystem;

        void Awake()
        {
            taskSystem = new TaskSystem();

            Instantiate(crewmate, new Vector3(-1.5f, 1.5f, 7), Quaternion.identity);
            //Redundant
            //CrewmateTaskAI crewmateTaskAI = crewmate.gameObject.GetComponent<CrewmateTaskAI>();
            //Crewmate cm = crewmate.gameObject.GetComponent<Crewmate>();
            //crewmateTaskAI.Setup(cm, taskSystem);
            //
            TaskSystem.Task task = new TaskSystem.Task { targetPosition = new Vector3(2.5f, 1.5f, -7) };
            taskSystem.AddTask(task);
        }
    }
}

