using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Crewmates
{
    public class SpyglassCameraController : MonoBehaviour
    {
        private CinemachineVirtualCamera virtualCamera;

        public GameObject orbitCamDistance;

        private float heldTime = 0.25f;

        private void Awake()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                heldTime -= Time.deltaTime;
                if (heldTime < 0)
                    virtualCamera.Priority = 99;
            }
            else
            {
                heldTime = 0.25f;
                virtualCamera.Priority = 0;
            }
        }
    }
}


