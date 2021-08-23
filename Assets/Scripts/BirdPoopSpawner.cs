using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crewmates
{
    public class BirdPoopSpawner : MonoBehaviour
    {
        [SerializeField] GameObject birdPoopPrefab;
        private float frequency = 10;

        private void Start()
        {
            InvokeRepeating("SpawnBirdPoop", frequency, frequency);
        }

        Vector3 GetRandomPosition()
        {
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

            int t = Random.Range(0, navMeshData.indices.Length - 3);

            Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
            Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);

            return point;
        }

        int GetRandomRotation()
        {
            int[] randomRotation = new int[] { 0, 90, 180, 270 };
            return randomRotation[UnityEngine.Random.Range(0, randomRotation.Length)];
        }

        private void SpawnBirdPoop()
        {
            int[] randomRotation = new int[] { 0, 90, 180, 270 };
            Instantiate(birdPoopPrefab, GetRandomPosition() + Vector3.up / 100, Quaternion.Euler(90, 0, GetRandomRotation()));
        }
    }
}

