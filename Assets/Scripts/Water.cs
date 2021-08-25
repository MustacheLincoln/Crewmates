using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Crewmates
{
    public class Water : Consumable, IReadiable
    {
        private float modifierDuration = 100;

        public override void Consume(Crewmate crewmate)
        {
            storedIn.items--;
            Transform waterLevel = storedIn.transform.Find("WaterLevel");
            waterLevel.position -= new Vector3(0, .1f, 0);
            crewmate.hydration += modifierDuration;
            Destroy(gameObject);
        }

        public void Ready()
        {

        }
    }
}
