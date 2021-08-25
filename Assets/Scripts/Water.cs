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
            storedIn.items--;
            storedIn.transform.GetChild(0).transform.position -= new Vector3(0, .1f, 0);
            crewmate.hydration += modifierDuration;
            Destroy(gameObject);
        }
    }
}
