using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class CannonBall : MonoBehaviour
    {
        private float damage = 25;

        private void Awake()
        {
            Destroy(gameObject, 30);
        }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.transform.parent)
            {
                GameObject other = collision.gameObject.transform.parent.gameObject;
                if (other.CompareTag("Enemy"))
                {
                    other.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            Destroy(gameObject);
        }
    }
}

