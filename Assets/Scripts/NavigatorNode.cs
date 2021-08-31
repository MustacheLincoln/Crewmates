using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crewmates
{
    public class NavigatorNode : MonoBehaviour
    {
        public Image background;
        public Image icon;
        public Image radialProgress;
        private bool targeted;
        private float warmUpTime = 1;
        private float targetTime =1;

        void Update()
        {
            if (targeted == false)
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
                background.transform.position = pos;
                icon.transform.position = pos;
                radialProgress.transform.position = pos;
                bool isPos = (pos.z >= 0);
                background.enabled = isPos;
                icon.enabled = isPos;
                radialProgress.enabled = isPos;
                if (GameManager.Instance.targeting)
                {
                    if (Vector3.Distance(transform.position, GameManager.Instance.playerShip.transform.position) <= GameManager.Instance.maxRange)
                    {
                        if (pos.x > Screen.width / 3 && pos.x < 2 * Screen.width / 3 && pos.z > 0)
                        {
                            warmUpTime -= Time.deltaTime;
                            if (warmUpTime < 0)
                            {
                                if (GameManager.Instance.targetingTarget == null || GameManager.Instance.targetingTarget == this.gameObject)
                                {
                                    if (GameManager.Instance.targetingTarget == null)
                                        GameManager.Instance.targetingTarget = this.gameObject;
                                    targetTime -= Time.deltaTime;
                                    radialProgress.fillAmount += Time.deltaTime;
                                    if (targetTime < 0)
                                    {
                                        Target();
                                    }
                                }
                            }
                            else
                            {
                                targetTime = 1f;
                                radialProgress.fillAmount = 0;
                                if (GameManager.Instance.targetingTarget == this.gameObject)
                                    GameManager.Instance.targetingTarget = null;
                            }
                        }
                        else
                        {
                            targetTime = 1f;
                            warmUpTime = 1;
                            radialProgress.fillAmount = 0;
                            if (GameManager.Instance.targetingTarget == this.gameObject)
                                GameManager.Instance.targetingTarget = null;
                        }
                    }
                    else
                    {
                        targetTime = 1f;
                        warmUpTime = 1;
                        radialProgress.fillAmount = 0;
                        if (GameManager.Instance.targetingTarget == this.gameObject)
                            GameManager.Instance.targetingTarget = null;
                    }
                }
                else
                {
                    targetTime = 1f;
                    warmUpTime = 1;
                    radialProgress.fillAmount = 0;
                    if (GameManager.Instance.targetingTarget == this.gameObject)
                        GameManager.Instance.targetingTarget = null;
                }
            }
            else
            {
                targetTime = 1f;
                warmUpTime = 1;
                radialProgress.fillAmount = 1;
                var tempColor = background.color;
                tempColor.a -= 5 * Time.deltaTime;
                tempColor.a = Mathf.Clamp(tempColor.a, 0, 1);
                background.color = tempColor;
                var tempColor1 = icon.color;
                tempColor1.a -= 5 * Time.deltaTime;
                tempColor1.a = Mathf.Clamp(tempColor1.a, 0, 1);
                icon.color = tempColor1;
                var tempColor2 = radialProgress.color;
                tempColor2.a -= 5 * Time.deltaTime;
                tempColor2.a = Mathf.Clamp(tempColor2.a, 0, 1);
                radialProgress.color = tempColor2;
                if (GameManager.Instance.targetingTarget == this.gameObject)
                    GameManager.Instance.targetingTarget = null;
            }

        }

        private void Target()
        {
            targeted = true;
            GameManager.Instance.playerShip.GetComponent<ShipMovement>().target = this.gameObject.transform;
        }
    }
}

