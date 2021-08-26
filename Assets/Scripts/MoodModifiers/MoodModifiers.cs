using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class MoodModifiers : MonoBehaviour
    {
        public Crewmate crewmate;
        public string modifierName;
        public int modifierAmount;
        public int modifierDuration;

        public string modifierDescription;
        public float remainingDuration;
        public bool going = false;

        private void Awake()
        {
            crewmate = GetComponent<Crewmate>();
        }

        public void Begin()
        {
            if (going == false)
            {
                going = true;
                remainingDuration = modifierDuration;

                crewmate.modifiers += modifierAmount;
                if (modifierAmount >= 0)
                    modifierDescription = modifierName + ": +" + modifierAmount;
                else
                    modifierDescription = modifierName + ": " + modifierAmount;
                crewmate.modifierDescriptions.Add(modifierDescription);
            }
        }

        private void Update()
        {
            if (going == true)
            {
                if (modifierDuration != 0)
                {
                    remainingDuration -= Time.deltaTime;
                    if (remainingDuration <= 0)
                        End();
                }
            }
        }

        public void End()
        {
            if (going == true)
            {
                going = false;
                crewmate.modifiers -= modifierAmount;
                while (crewmate.modifierDescriptions.Contains(modifierDescription))
                    crewmate.modifierDescriptions.Remove(modifierDescription);
            }
        }
    }
}

