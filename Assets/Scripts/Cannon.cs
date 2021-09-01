using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Cannon : MonoBehaviour, ITask, IReadiable
    {
        private bool beingUsed;
        public GameObject cannonBall;
        public Crewmate stationed;
        public Transform muzzle;
        public Transform cannonBarrel;
        public Transform cannonBase;
        public float muzzleVelocity = 100;

        private float loadTime = 5;
        private float turnSpeed = 2;

        private void Start()
        {
            Ready();//?
        }

        public void Ready()
        {
            if (!GameManager.Instance.battleTasks.Contains(this.gameObject))
                GameManager.Instance.battleTasks.Add(this.gameObject);
            transform.Find("CannonBarrel").gameObject.layer = 0; //TEMP
            transform.Find("CannonBase").gameObject.layer = 0; //TEMP
        }

        public void Task(Crewmate crewmate)
        {
            beingUsed = true;
            if (ClosestEnemy())
            {
                crewmate.MoveTo(transform.position, () =>
                {
                    stationed = crewmate;
                });
            }
            else
            {
                crewmate.myTask = null;
                stationed = null;
                beingUsed = false;
                if (!GameManager.Instance.battleTasks.Contains(this.gameObject))
                    GameManager.Instance.battleTasks.Add(this.gameObject);
            }
        }

        private void Update()
        {
            if (ClosestEnemy())
            {
                if (stationed)
                {
                    var ar = AimRotation(muzzle.position, ClosestEnemy().transform.position, muzzleVelocity);
                    transform.rotation = Quaternion.Lerp(transform.rotation, ar, turnSpeed * Time.deltaTime);
                    loadTime -= Time.deltaTime;
                    if (loadTime < 0 && transform.rotation == ar)
                    {
                        FireCannonAtPoint(ClosestEnemy().transform.position);
                        loadTime = 5;
                    }

                }
            }
            else
            {
                if (stationed)
                {
                    stationed.myTask = null;
                    //Celebrate animation
                    stationed = null;
                    beingUsed = false;
                    if (!GameManager.Instance.battleTasks.Contains(this.gameObject))
                        GameManager.Instance.battleTasks.Add(this.gameObject);
                }
            }


        }

        private GameObject ClosestEnemy()
        {
            GameObject closestEnemy = null;
            float closestDist = Mathf.Infinity;
            if (GameManager.Instance.targetedEnemies.Count > 0)
            {
                foreach (GameObject enemy in GameManager.Instance.targetedEnemies)
                {
                    float dist = (enemy.transform.position - transform.position).magnitude;
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closestEnemy = enemy;
                    }
                }
                return closestEnemy;
            }
            return closestEnemy;
        }

        private void FireCannonAtPoint(Vector3 point)
        {
            GameObject cb = Instantiate(cannonBall);
            Rigidbody cbrb = cb.GetComponent<Rigidbody>();

            cbrb.transform.position = muzzle.position;
            cbrb.transform.rotation = transform.rotation;
            cbrb.velocity = transform.forward * (muzzleVelocity + Random.Range(-10, 10));
        }

        public Quaternion AimRotation(Vector3 start, Vector3 end, float velocity)
        {

            float low;
            float high;
            Ballistics.CalculateTrajectory(start, end, velocity, out low, out high); //get the angle


            Vector3 wantedRotationVector = Quaternion.LookRotation(end - start).eulerAngles; //get the direction
            wantedRotationVector.x += low; //combine the two
            return Quaternion.Euler(wantedRotationVector); //into a quaternion
        }
    }
}
