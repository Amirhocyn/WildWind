using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using WildWind.Movement;

namespace WildWind.Control
{
    
    public class MissileController : MonoBehaviourMaster<MissileController>
    {

        private Transform target;
        [SerializeField] private MoverData moverData;
        [SerializeField] private ObjectType _moverType;
        private IMover _mover;
        public IMover mover
        {
            get
            {

                if (_mover == null)
                    _mover = Activator.CreateInstance(_moverType.type) as IMover;

                return _mover;

            }
            set
            {
                _mover = value as IMover;
            }
        }

        [SerializeField] float lifeTime = 30;
        public static Action onDestroy;
        float angleBetween = 0;

        public override void Start()
        {

            base.Start();
            SetTarget();
            StartCoroutine(WaitForEndOfLife());

        }

        public void Update()
        {

            if (target != null)
            {

                angleBetween = GetAngleBetweenMissileAndTarget();
                //angleBetween = Mathf.Clamp(angleBetween * 3f / moverData.yawRate, -1, 1);
                mover.Execute(moverData, transform, (Math.Sign(angleBetween)));

            }
            else
            {

                mover.Execute(moverData, transform, 0);

            }

        }

        IEnumerator WaitForEndOfLife()
        {

            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);

        }

        private float GetAngleBetweenMissileAndTarget()
        {

            return Vector3.SignedAngle(new Vector3(transform.forward.x, 0, transform.forward.z), (new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z)).normalized, transform.up);

        }

        private void SetTarget()
        {

            if (FindObjectOfType<PlayerController>() != null)
                target = FindObjectOfType<PlayerController>().transform;

        }


        public override void OnDestroy()
        {

            base.OnDestroy();
            if (onDestroy != null)
                onDestroy();

        }

    }

}
