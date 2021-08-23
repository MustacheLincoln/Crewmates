using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crewmates
{
    public class BirdPoopSpawner : MonoBehaviour
    {
        [SerializeField] GameObject birdPoopPrefab;
        private GameManager gm;

        private void Awake()
        {
            gm = FindObjectOfType<GameManager>();
            Invoke("SpawnBirdPoop", UnityEngine.Random.Range(10, 20));
        }

        private void SpawnBirdPoop()
        {
            Instantiate(birdPoopPrefab, gm.GetRandomPosition() + Vector3.up / 100, Quaternion.Euler(90, 0, gm.GetRandomRotation()));
            Invoke("SpawnBirdPoop", UnityEngine.Random.Range(10, 20));
        }
    }
}

