using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crewmates
{
    public class Cannon : MonoBehaviour, ITask, IReadiable
    {
        public GameObject cannonBall;
        public Crewmate stationed;
        public Transform muzzle;
        public Transform cannonBarrel;
        public Transform cannonBase;
        public float muzzleVelocity = 100;

        private float rotationRange = 75;
        private float loadTime = 5;
        private float turnSpeed = 3;
        private Quaternion defaultRotation;

        private void Start()
        {
            Ready();//?
            defaultRotation = transform.localRotation;
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
            if (OptimalEnemy())
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
                if (!GameManager.Instance.battleTasks.Contains(this.gameObject))
                    GameManager.Instance.battleTasks.Add(this.gameObject);
            }
        }

        private void Update()
        {
            if (OptimalEnemy())
            {
                if (stationed)
                {
                    Quaternion aimRotation = AimRotation(muzzle.position, OptimalEnemy().transform.position, muzzleVelocity);
                    Vector3 rotationEulers = aimRotation.eulerAngles;
                    rotationEulers.x -= 90;
                    if (Quaternion.Angle(defaultRotation * transform.parent.rotation, aimRotation) <= rotationRange)
                    {
                        cannonBarrel.rotation = Quaternion.Lerp(cannonBarrel.rotation, Quaternion.Euler(rotationEulers), turnSpeed * Time.deltaTime);
                        rotationEulers.x = -90;
                        cannonBase.rotation = Quaternion.Lerp(cannonBase.rotation, Quaternion.Euler(rotationEulers), turnSpeed * Time.deltaTime);
                    }
                    loadTime -= Time.deltaTime;
                    if (loadTime < 0 && Quaternion.Angle(cannonBarrel.rotation, Quaternion.Euler(rotationEulers)) < 20)
                    {
                        FireCannonAtPoint(OptimalEnemy().transform.position);
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
                    if (!GameManager.Instance.battleTasks.Contains(this.gameObject))
                        GameManager.Instance.battleTasks.Add(this.gameObject);
                }
            }


        }

        private GameObject OptimalEnemy()
        {
            GameObject closestEnemy = null;
            float closestDist = Mathf.Infinity;
            if (GameManager.Instance.targetedEnemies.Count > 0)
            {
                foreach (GameObject enemy in GameManager.Instance.targetedEnemies)
                {
                    if (Quaternion.Angle(AimRotation(muzzle.position, enemy.transform.position, muzzleVelocity), defaultRotation * transform.parent.rotation) > rotationRange)
                        continue;
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
            cbrb.transform.rotation = cannonBarrel.rotation;
            cbrb.velocity = -cannonBarrel.up * (muzzleVelocity + Random.Range(-10, 10));
        }

        public Quaternion AimRotation(Vector3 start, Vector3 end, float velocity)
        {
            float angle;
            Ballistics.CalculateTrajectory(start, end, velocity, out angle); //get the angle

            Vector3 wantedRotationVector = Quaternion.LookRotation(end - start).eulerAngles; //get the direction
            wantedRotationVector.x += angle; //combine the two
            return Quaternion.Euler(wantedRotationVector); //into a quaternion
        }
    }
}
