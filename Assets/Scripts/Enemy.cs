using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crewmates
{
    public class Enemy : MonoBehaviour
    {
        private float targetTime = 1;
        private float warmUpTime = 1;
        private bool targeted;

        private float health = 100;

        public Image targetingProgressRadial;
        public Image pirateIcon;

        private void Update()
        {
            if (targeted == false)
            {
                if (GameManager.Instance.targeting)
                {
                    if (Vector3.Distance(transform.position, GameManager.Instance.playerShip.transform.position) <= GameManager.Instance.maxRange)
                    {
                        var pos = Camera.main.WorldToScreenPoint(transform.position);
                        targetingProgressRadial.transform.position = pos;
                        pirateIcon.transform.position = pos;
                        if (pos.x > Screen.width / 3 && pos.x < 2 * Screen.width / 3 && pos.z > 0)
                        {

                            var tempColor = pirateIcon.color;
                            tempColor.a += 5 * Time.deltaTime;
                            tempColor.a = Mathf.Clamp(tempColor.a, 0, 1);
                            pirateIcon.color = tempColor;

                            warmUpTime -= Time.deltaTime;
                            if (warmUpTime < 0)
                            {
                                if (GameManager.Instance.targetingTarget == null || GameManager.Instance.targetingTarget == this.gameObject)
                                {
                                    if (GameManager.Instance.targetingTarget == null)
                                        GameManager.Instance.targetingTarget = this.gameObject;
                                    targetTime -= Time.deltaTime;
                                    targetingProgressRadial.fillAmount += Time.deltaTime;
                                    if (targetTime < 0)
                                    {
                                        Target();
                                    }
                                }
                            }
                            else
                            {
                                targetTime = 1f;
                                targetingProgressRadial.fillAmount = 0;
                                if (GameManager.Instance.targetingTarget == this.gameObject)
                                    GameManager.Instance.targetingTarget = null;
                            }
                        }
                        else
                        {
                            var tempColor = pirateIcon.color;
                            tempColor.a -= 5 * Time.deltaTime;
                            tempColor.a = Mathf.Clamp(tempColor.a, 0, 1);
                            pirateIcon.color = tempColor;
                            targetTime = 1f;
                            warmUpTime = 1;
                            targetingProgressRadial.fillAmount = 0;
                            if (GameManager.Instance.targetingTarget == this.gameObject)
                                GameManager.Instance.targetingTarget = null;
                        }
                    }
                    else
                    {
                        var tempColor = pirateIcon.color;
                        tempColor.a -= 5 * Time.deltaTime;
                        tempColor.a = Mathf.Clamp(tempColor.a, 0, 1);
                        pirateIcon.color = tempColor;
                        targetTime = 1f;
                        warmUpTime = 1;
                        targetingProgressRadial.fillAmount = 0;
                        if (GameManager.Instance.targetingTarget == this.gameObject)
                            GameManager.Instance.targetingTarget = null;
                    }
                }
                else
                {
                    var tempColor = pirateIcon.color;
                    tempColor.a -= 5 * Time.deltaTime;
                    tempColor.a = Mathf.Clamp(tempColor.a, 0, 1);
                    pirateIcon.color = tempColor;
                    targetTime = 1f;
                    warmUpTime = 1;
                    targetingProgressRadial.fillAmount = 0;
                    if (GameManager.Instance.targetingTarget == this.gameObject)
                        GameManager.Instance.targetingTarget = null;
                }
            }
            else
            {
                var tempColor = pirateIcon.color;
                tempColor.a -= 5 * Time.deltaTime;
                tempColor.a = Mathf.Clamp(tempColor.a, 0, 1);
                pirateIcon.color = tempColor;
                var tempColor1 = targetingProgressRadial.color;
                tempColor1.a -= 5 * Time.deltaTime;
                tempColor1.a = Mathf.Clamp(tempColor1.a, 0, 1);
                targetingProgressRadial.color = tempColor1;
                targetTime = 1f;
                warmUpTime = 1;
                targetingProgressRadial.fillAmount = 1;
                if (GameManager.Instance.targetingTarget == this.gameObject)
                    GameManager.Instance.targetingTarget = null;
                if (Vector3.Distance(transform.position, GameManager.Instance.playerShip.transform.position) > GameManager.Instance.maxRange)
                    Untarget();
            }
        }

        private void Target()
        {
            targeted = true;
            if (GameManager.Instance.targetingTarget == this.gameObject)
                GameManager.Instance.targetingTarget = null;
            if (!GameManager.Instance.targetedEnemies.Contains(this.gameObject))
                GameManager.Instance.targetedEnemies.Add(this.gameObject);
        }

        private void Untarget()
        {
            targeted = false;
            if (GameManager.Instance.targetedEnemies.Contains(this.gameObject))
                GameManager.Instance.targetedEnemies.Remove(this.gameObject);
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
                Die();
        }

        private void Die()
        {
            Untarget();
            Destroy(gameObject);
        }
    }
}

