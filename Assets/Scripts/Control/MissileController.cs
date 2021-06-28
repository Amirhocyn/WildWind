using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WildWind.Movement;

namespace WildWind.Control
{

    public class MissileController : MonoBehaviour
    {

        Transform target;
        Mover mover;
        [SerializeField] float lifeTime = 30;

        private void Start()
        {

            SetTarget();
            SetMover();
            StartCoroutine(WaitForEndOfLife());

        }     

        private void Update()
        {

            if (target != null)
            {

                float angleBetween = GetAngleBetweenMissileAndTarget();
                InteractWithMover(angleBetween);

            }

        }
        IEnumerator WaitForEndOfLife()
        {

            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);

        }

        private void InteractWithMover(float angleBetween)
        {

            mover.Turn(Mathf.Clamp(angleBetween * 3 / mover.GetTurnAngle(), -1, 1));

        }

        private float GetAngleBetweenMissileAndTarget()
        {

            return Vector3.SignedAngle(new Vector3(transform.forward.x, 0, transform.forward.z), (new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z)).normalized, transform.up);

        }

        private void SetMover()
        {

            mover = GetComponent<Mover>();

        }

        private void SetTarget()
        {

            target = FindObjectOfType<PlayerController>().transform;

        }

    }

}
