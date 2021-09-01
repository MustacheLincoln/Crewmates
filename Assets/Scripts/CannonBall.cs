using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class CannonBall : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(gameObject, 30);
        }
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.transform.parent)
                if (GameManager.Instance.targetedEnemies.Contains(collision.gameObject.transform.parent.gameObject))
                    GameManager.Instance.targetedEnemies.Remove(collision.gameObject.transform.parent.gameObject);
            Destroy(collision.gameObject.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}

