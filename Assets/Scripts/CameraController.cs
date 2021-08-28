using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class CameraController : MonoBehaviour
    {
        public GameObject camFocus;

        private float zoomSpeed = 20;
        private float rotateSpeed = 5;
        private void Update()
        {
            transform.localPosition -= new Vector3(Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, 0,Input.GetAxis("Mouse ScrollWheel")*zoomSpeed);
            if (transform.localPosition.z < 0)
            {
                transform.localPosition += new Vector3(Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);
            }
            if (Input.GetMouseButton(2))
                camFocus.transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);
        }
    }


}

