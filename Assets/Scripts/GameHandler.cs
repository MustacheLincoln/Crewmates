using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class GameHandler : MonoBehaviour
    {
        public GameObject crewmate;

        void Start()
        {
            TaskSystem taskSystem = new TaskSystem();

            Instantiate(crewmate, new Vector3(-1.5f, 1.5f, 7), Quaternion.identity);
            CrewmateTaskAI crewmateTaskAI = crewmate.gameObject.AddComponent<CrewmateTaskAI>();
            Crewmate cm = crewmate.gameObject.AddComponent<Crewmate>();
            crewmateTaskAI.Setup(cm, taskSystem);
            TaskSystem.Task task = new TaskSystem.Task { targetPosition = new Vector3(2.5f, 1.5f, -7) };
        }
    }
}
