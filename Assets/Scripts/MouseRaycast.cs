using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crewmates
{
    public class MouseRaycast : MonoBehaviour
    {
        public Vector3 walkablePosition;
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

        public Vector3 HitPosition()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
                return hit.point;
            else return Vector3.zero;
        }

        public GameObject HitObject()
        {
            Ray objRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit objHit;
            if (Physics.Raycast(objRay, out objHit, float.MaxValue, layerMask))
            {
                return objHit.transform.gameObject;
            }
            else return null;
        }

        public bool Walkable()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
            {
                NavMeshHit navHit;
                if (NavMesh.SamplePosition(hit.point, out navHit, 1.0f, NavMesh.AllAreas))
                {
                    walkablePosition = navHit.position;
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

