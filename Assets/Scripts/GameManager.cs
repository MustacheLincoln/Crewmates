using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] MouseRaycast mouseRaycast;
        [SerializeField] GameObject birdPoopPrefab;
        public List<Crewmate> crewmates;
        public List<GameObject> tasks;
        public List<Crate> crates;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (mouseRaycast.Walkable())
                {
                    SpawnBirdPoop(mouseRaycast.hitPosition);
                }
            }
        }

        private void SpawnBirdPoop(Vector3 mousePosition)
        {
            int[] randomRotation = new int[] {0, 90, 180, 270};
            Instantiate(birdPoopPrefab, mousePosition+Vector3.up/100, Quaternion.Euler(90,0, randomRotation[UnityEngine.Random.Range(0, randomRotation.Length)]));
        }
    }
}
