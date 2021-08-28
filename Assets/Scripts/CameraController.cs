using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class CameraController : MonoBehaviour
    {
        public GameObject camFocus;

        private float zoomSpeed = 750;
        private float rotateSpeed = 250;
        private void Update()
        {
            transform.localPosition -= new Vector3(Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime);
            if (transform.localPosition.z < 0)
            {
                transform.localPosition += new Vector3(Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime);
            }
            if (Input.GetMouseButton(2))
                camFocus.transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, 0);

            float edgeSize = 8;
            float zoomAmount = 25;
            float rotateAmount = 50;
            if (Input.mousePosition.x > Screen.width - edgeSize)
                camFocus.transform.rotation *= Quaternion.Euler(0, -rotateAmount * Time.deltaTime, 0);
            if (Input.mousePosition.x < edgeSize)
                camFocus.transform.rotation *= Quaternion.Euler(0, rotateAmount * Time.deltaTime, 0);
            if (Input.mousePosition.y > Screen.height - edgeSize)
                if (transform.localPosition.z > 0)
                    transform.localPosition -= new Vector3(zoomAmount * Time.deltaTime, 0, zoomAmount * Time.deltaTime);
            if (Input.mousePosition.y < edgeSize)
                transform.localPosition += new Vector3(zoomAmount * Time.deltaTime, 0, zoomAmount * Time.deltaTime);
        }
    }


}

