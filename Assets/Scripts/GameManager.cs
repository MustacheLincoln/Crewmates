using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crewmates
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private MouseRaycast mouseRaycast;
        [SerializeField] private UI UI;
        public List<Crewmate> crewmates;
        public List<GameObject> globalTasks;
        public List<Crate> crates;
        public GameObject placing;
        public GameObject selected;
        private GameObject tempSelected;
        public GameObject rightClicking;

        private void Update()
        {
            if (placing)
            {
                if (mouseRaycast.Walkable())
                {
                    placing.transform.position = mouseRaycast.hitPosition;
                    if (Input.GetMouseButtonUp(0))
                    {   
                        placing.GetComponent<IReadiable>().Ready();
                        placing = null;
                    }
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                tempSelected = null;
                if (mouseRaycast.hitObject.transform.parent)
                    if (mouseRaycast.hitObject.transform.parent.GetComponent<Crewmate>())
                        tempSelected = mouseRaycast.hitObject.transform.parent.gameObject;
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (rightClicking)
                {
                    rightClicking = null;
                    tempSelected = null;
                    return;
                }
                if (selected)
                    selected = null;
                if (tempSelected)
                    if (mouseRaycast.hitObject.transform.parent.gameObject == tempSelected)
                    {
                        selected = mouseRaycast.hitObject.transform.parent.gameObject;
                        tempSelected = null;
                    }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (rightClicking)
                    rightClicking = null;
                if (mouseRaycast.hitObject.transform.parent)
                {
                    rightClicking = mouseRaycast.hitObject.transform.parent.gameObject;
                    UI.ContextMenu(Input.mousePosition, rightClicking);
                }
            }
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
            string[] firstName = new string[] { "Henry", "John", "Bill", "Jack", "Pete", "William", "Ishmael", "Jonah", "Newt", "Wilhelm", "Abraham", "Asa", "Archibald", "Guillermo", "Corvo" };
            string[] lastName = new string[] { "Smith", "Silver", "Wallace", "Black", "Carver", "Forsythe", "Phelps", "Sanchez", "Puck", "Cooper", "Fletcher", "Carter", "Addams", "Robinson" };

            return firstName[UnityEngine.Random.Range(0, firstName.Length)] + " " + lastName[UnityEngine.Random.Range(0, lastName.Length)];
        }

        public void Place(GameObject gameObject)
        {
            if (placing == null)
                placing = Instantiate(gameObject);
            else
            {
                Destroy(placing);
                placing = Instantiate(gameObject);
            }
        }
    }

}
