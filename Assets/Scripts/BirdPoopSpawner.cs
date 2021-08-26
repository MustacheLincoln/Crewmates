using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crewmates
{
    public class BirdPoopSpawner : MonoBehaviour
    {
        [SerializeField] GameObject birdPoopPrefab;
        private GameManager gameManager;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            Invoke("SpawnBirdPoop", UnityEngine.Random.Range(10, 20));
        }

        private void SpawnBirdPoop()
        {
            Instantiate(birdPoopPrefab, gameManager.GetRandomPosition() + Vector3.up / 100, Quaternion.Euler(90, 0, gameManager.GetRandomRotation()));
            Invoke("SpawnBirdPoop", UnityEngine.Random.Range(10, 20));
        }
    }
}

