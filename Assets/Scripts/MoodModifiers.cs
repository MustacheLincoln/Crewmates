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

        private string modifierDescription;
        private float remainingDuration;
        private bool going = false;
        private int index;

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
                index = crewmate.modifierDescriptions.Count;
            }
        }

        private void Update()
        {
            if (going == true)
            {
                if (modifierDuration != 0)
                {
                    remainingDuration -= Time.deltaTime;
                    if (modifierAmount >= 0)
                        modifierDescription = modifierName + ": +" + modifierAmount + " for " + (int)remainingDuration + "sec";
                    else
                        modifierDescription = modifierName + ": " + modifierAmount + " for " + (int)remainingDuration + "sec";
                    if (index == crewmate.modifierDescriptions.Count)
                        crewmate.modifierDescriptions.Add(modifierDescription);
                    crewmate.modifierDescriptions[index] = modifierDescription;
                    if (remainingDuration <= 0)
                        End();
                }
                else
                {
                    if (modifierAmount >= 0)
                        modifierDescription = modifierName + ": +" + modifierAmount;
                    else
                        modifierDescription = modifierName + ": " + modifierAmount + " for ";
                    if (index == crewmate.modifierDescriptions.Count)
                        crewmate.modifierDescriptions.Add(modifierDescription);
                    crewmate.modifierDescriptions[index] = modifierDescription;
                }

            }
        }

        public void End()
        {
            if (going == true)
            {
                going = false;
                crewmate.modifiers -= modifierAmount;
                if (crewmate.modifierDescriptions.Contains(modifierDescription))
                    crewmate.modifierDescriptions.Remove(modifierDescription);
            }
        }
    }
}

