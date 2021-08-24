using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Crewmates
{
    public class Water : Consumable
    {
        private float modifierDuration = 100;

        public override void Consume(Crewmate crewmate)
        {
            if (storedIn)
                storedIn.items--;
            crewmate.hydration += modifierDuration;
            Destroy(gameObject);
        }
    }
}
