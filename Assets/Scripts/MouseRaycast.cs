using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crewmates
{
    public class MouseRaycast : MonoBehaviour
    {
        public Vector3 hitPosition;
        public GameObject hitObject;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask layerMask;

        public static MouseRaycast Instance { get; private set; }

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

        void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, float.MaxValue))
                {
                    hitObject = hit.transform.gameObject;
                }
            }
        }

        public bool Walkable()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
            {
                NavMeshHit navHit;
                if (NavMesh.SamplePosition(hit.point, out navHit, 1.0f, NavMesh.AllAreas))
                {
                    hitPosition = navHit.position;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}

