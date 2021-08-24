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
        [SerializeField] GameObject cratePrefab;
        [SerializeField] GameObject barrelPrefab;
        public List<Crewmate> crewmates;
        public List<GameObject> globalTasks;
        public List<Crate> crates;
        public List<Barrel> barrels;
        public List<Rum> rum;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (mouseRaycast.Walkable())
                {
                    SpawnBarrel(mouseRaycast.hitPosition);
                }
            }
        }

        private void SpawnBarrel(Vector3 mousePosition)
        {
            Instantiate(barrelPrefab, mousePosition + new Vector3(0, .417f, 0), Quaternion.Euler(-90, 0, 0));
        }

        private void SpawnCrate(Vector3 mousePosition)
        {
            Instantiate(cratePrefab, mousePosition + new Vector3(0,.417f,0), Quaternion.identity);
        }

        private void SpawnRum(Vector3 mousePosition)
        {
            Instantiate(rumPrefab, mousePosition + new Vector3(0, .221f, 0), Quaternion.Euler(-90,0,0));
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

        public string GenerateName()
        {
            string[] firstName = new string[] { "John", "Bill" , "Jack", "Pete", "William", "Ishmael", "Jonah", "Newt", "Wilhelm", "Abraham", "Asa", "Archibald", "Guillermo", "Corvo" };
            string[] lastName = new string[] { "Smith", "Silver", "Wallace", "Black", "Carver", "Forsythe", "Phelps", "Sanchez", "Puck", "Cooper", "Fletcher", "Carter" };

            return firstName[UnityEngine.Random.Range(0, firstName.Length)]+" "+lastName[UnityEngine.Random.Range(0, lastName.Length)];
        }
    }

}
