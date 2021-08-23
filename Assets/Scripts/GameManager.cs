using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crewmates
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] MouseRaycast mouseRaycast;
        [SerializeField] GameObject rumPrefab;
        public List<Crewmate> crewmates;
        public List<GameObject> globalTasks;
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
            Instantiate(rumPrefab, mousePosition + Vector3.up/5, Quaternion.Euler(-90,0,0));
        }
        public Vector3 GetRandomPosition()
        {
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

            int t = UnityEngine.Random.Range(0, navMeshData.indices.Length - 3);

            Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], UnityEngine.Random.value);
            Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], UnityEngine.Random.value);

            return point;
        }

        public int GetRandomRotation()
        {
            int[] randomRotation = new int[] { 0, 90, 180, 270 };
            return randomRotation[UnityEngine.Random.Range(0, randomRotation.Length)];
        }
    }

}
