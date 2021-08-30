using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Enemy : MonoBehaviour
    {
        private float targetTime = 2;
        private bool targeted;

        private void Update()
        {
            if (GameManager.Instance.targeting)
            {
                var pos = Camera.main.WorldToScreenPoint(transform.position);
                if (pos.x > 350 && pos.x < 700 && pos.z > 0)
                {
                    targetTime -= Time.deltaTime;
                    if (targetTime < 0)
                    {
                        if (targeted == false)
                        {
                            targeted = true;
                            print("Targeted " + gameObject);
                        }
                    }
                }
                else targetTime = 2;
            }
            else targetTime = 2;
        }
    }
}

