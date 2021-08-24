using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Barrel : Storage
    {
        private GameManager gm;
        [SerializeField] private GameObject waterPrefab;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
            gm.barrels.Add(this);
            maxItems = 8;
            items = maxItems;
        }

        private void Start()
        {
            for (int i = 0; i < maxItems; i++)
            {
                Instantiate(waterPrefab, transform);
            }
        }

    }
}
