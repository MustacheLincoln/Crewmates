using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Barrel : Storage, IReadiable
    {
        private GameManager gameManager;
        [SerializeField] private GameObject waterPrefab;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
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
            gameManager.barrels.Add(this);
            var water = GetComponentsInChildren<Water>();
            foreach (Water w in water)
                w.Ready();
        }
    }
}