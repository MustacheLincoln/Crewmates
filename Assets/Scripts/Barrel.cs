using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Barrel : Storage, IReadiable
    {
        [SerializeField] private GameObject waterPrefab;

        private void Awake()
        {
            maxItems = 8;
            items = maxItems;
        }

        private void Start()
        {
            for (int i = 0; i < maxItems; i++)
            {
                Transform waterLevel = transform.Find("WaterLevel");
                GameObject water = Instantiate(waterPrefab, waterLevel.position, Quaternion.identity, waterLevel);
                water.GetComponent<Water>().storedIn = this;
            }
        }

        public void Ready()
        {
            var water = GetComponentsInChildren<Water>();
            transform.Find("Mesh").gameObject.layer = 0; //TEMP
            foreach (Water w in water)
                w.Ready();
        }
    }
}