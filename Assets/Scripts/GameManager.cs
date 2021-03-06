using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crewmates
{
    public class GameManager : MonoBehaviour
    {
        public GameObject playerShip;
        public List<Crewmate> crewmates;
        public List<GameObject> globalTasks;
        public List<GameObject> battleTasks;
        public List<Crate> crates;
        public List<GameObject> targetedEnemies;
        public GameObject placing;
        public GameObject selected;
        private GameObject tempSelected;
        public GameObject rightClicking;
        public bool targeting;
        public GameObject targetingTarget;
        public int maxRange = 1000;


        //Temp while waiting to spend $100
        //public GameObject floor;

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void Update()
        {
            targetedEnemies.RemoveAll(x => !x);
            if (placing)
            {
                if (MouseRaycast.Instance.Walkable())//(MouseRaycast.Instance.HitObject() == floor)
                {
                    placing.transform.position = MouseRaycast.Instance.walkablePosition;//HitPosition()
                    if (Input.GetMouseButton(0))
                    {
                        placing.GetComponent<IReadiable>().Ready();
                        placing = null;
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    tempSelected = null;
                    if (MouseRaycast.Instance.HitObject())
                        if (MouseRaycast.Instance.HitObject().transform.parent)
                            if (MouseRaycast.Instance.HitObject().transform.parent.GetComponent<Crewmate>())
                                tempSelected = MouseRaycast.Instance.HitObject().transform.parent.gameObject;
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
                        if (MouseRaycast.Instance.HitObject())
                            if (MouseRaycast.Instance.HitObject().transform.parent.gameObject == tempSelected)
                            {
                                selected = MouseRaycast.Instance.HitObject().transform.parent.gameObject;
                                tempSelected = null;
                            }
                }

                if (Input.GetMouseButtonDown(1))
                {
                    if (rightClicking)
                        rightClicking = null;
                    if(MouseRaycast.Instance.HitObject())
                    if (MouseRaycast.Instance.HitObject().transform.parent)
                    {
                        rightClicking = MouseRaycast.Instance.HitObject().transform.parent.gameObject;
                        UI.Instance.ContextMenu(Input.mousePosition, rightClicking);
                    }
                }
            }
        }

        public Vector3 GetRandomPosition()
        {
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

            int t = UnityEngine.Random.Range(0, navMeshData.indices.Length - 3);

            Vector3 pos = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], UnityEngine.Random.value);
            Vector3.Lerp(pos, navMeshData.vertices[navMeshData.indices[t + 2]], UnityEngine.Random.value);

            return pos;
        }

        public int GetRandomRotation()
        {
            int[] randomRotation = new int[] { 0, 90, 180, 270 };
            return randomRotation[UnityEngine.Random.Range(0, randomRotation.Length)];
        }

        public string GenerateName()
        {
            string[] firstName = new string[] { "Henry", "John", "Bill", "Jack", "Pete", "William", "Ishmael", "Jonah", "Newt", "Wilhelm", "Abraham", "Asa", "Archibald", "Guillermo", "Corvo", "Moses", "Issac", "Pablo", "Clyde", "Ichabod", "Alexander", "Mordecai", "Moe", "Hans", "Davey", "Daniel", "Bjorn", "Erik" };
            string[] lastName = new string[] { "Smith", "Silver", "Wallace", "Black", "Carver", "Forsythe", "Phelps", "Sanchez", "Puck", "Cooper", "Fletcher", "Carter", "Addams", "Robinson", "Gunn", "Wood", "Jones", "Crusoe", "Stevenson", "Melville", "Defoe", "Carter", "Anderson", "Gustafson" };

            return firstName[UnityEngine.Random.Range(0, firstName.Length)] + " " + lastName[UnityEngine.Random.Range(0, lastName.Length)];
        }

        public void Place(GameObject gameObject)
        {
            if (placing == null)
                placing = Instantiate(gameObject, playerShip.transform);
            else
            {
                Destroy(placing);
                placing = Instantiate(gameObject, playerShip.transform);
            }
        }
    }

}
