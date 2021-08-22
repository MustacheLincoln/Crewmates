using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Crewmates
{
    public class MouseRaycast : MonoBehaviour
    {
        public Vector3 hitPosition;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask layerMask;

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

