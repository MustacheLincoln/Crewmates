using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class CrewmateMouseInteractions : MonoBehaviour
    {
        private Crewmate crewmate;

        private void Awake()
        {
            crewmate = GetComponentInParent<Crewmate>();
        }

        private void OnMouseEnter()
        {
            crewmate.MouseEnter();
        }

        private void OnMouseExit()
        {
            crewmate.MouseExit();
        }
    }
}

