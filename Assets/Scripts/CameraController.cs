using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

namespace Crewmates
{
    public class CameraController : MonoBehaviour
    {
        public GameObject camFocus;
        public Image spyGlassSprite;
        public CinemachineVirtualCamera virtualCamera;

        private float heldTime = 0.2f;
        private float zoomSpeed = 750;
        private float rotateSpeed = 250;
        private float povSpeedX = 100;

        private void LateUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                heldTime -= Time.deltaTime;
                if (heldTime < 0)
                {
                    virtualCamera.Priority = 0;
                    camFocus.transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * povSpeedX * Time.deltaTime, 0);
                    Cursor.lockState = CursorLockMode.Locked;
                    GameManager.Instance.targeting = true;
                    var tempColor = spyGlassSprite.color;
                    tempColor.a += .1f;
                    tempColor.a = Mathf.Clamp(tempColor.a, 0, 1);
                    spyGlassSprite.color = tempColor;
                }
            }
            else
            {
                heldTime = 0.2f;
                virtualCamera.Priority = 99;
                GameManager.Instance.targeting = false;
                var tempColor = spyGlassSprite.color;
                tempColor.a -= .1f;
                tempColor.a = Mathf.Clamp(tempColor.a, 0, 1);
                spyGlassSprite.color = tempColor;
                Cursor.lockState = CursorLockMode.None;
                transform.localPosition -= new Vector3(Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime);
                if (transform.localPosition.z < 5 || transform.localPosition.z >= 75)
                    transform.localPosition += new Vector3(Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime);

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
                    if (transform.localPosition.z > 5)
                        transform.localPosition -= new Vector3(zoomAmount * Time.deltaTime, 0, zoomAmount * Time.deltaTime);
                if (Input.mousePosition.y < edgeSize)
                    if (transform.localPosition.z <= 75)
                        transform.localPosition += new Vector3(zoomAmount * Time.deltaTime, 0, zoomAmount * Time.deltaTime);
            }
        }
    }
}

