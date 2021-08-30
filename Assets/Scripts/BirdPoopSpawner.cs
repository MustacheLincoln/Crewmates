using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crewmates
{
    public class BirdPoopSpawner : MonoBehaviour
    {
        [SerializeField] GameObject birdPoopPrefab;
        [SerializeField] GameObject boat;

        private void Awake()
        {
            Invoke("SpawnBirdPoop", UnityEngine.Random.Range(10, 20));
        }

        private void SpawnBirdPoop()
        {
            Instantiate(birdPoopPrefab, GameManager.Instance.GetRandomPosition() + Vector3.up / 100, Quaternion.Euler(90, 0, GameManager.Instance.GetRandomRotation()),boat.transform);
            Invoke("SpawnBirdPoop", UnityEngine.Random.Range(10, 20));
        }
    }
}

