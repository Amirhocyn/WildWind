using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WildWind.Movement
{

    public class Boost : MonoBehaviourMaster<Boost>
    {

        [SerializeField] float maxBoostTime = 10;
        [SerializeField] float boostSpeedMultiplier;
        private float baseSpeed;
        private float _remainingTime = 0;
        private float remainingTime
        {

            get
            {

                return _remainingTime;

            }

            set
            {

                _remainingTime = Mathf.Clamp(value, 0, maxBoostTime);

            }

        }
        private Mover mover;

        public override void Start()
        {

            SetMover();
            GetMoverNormalSpeed();          

        }      

        public override void Update()
        {

            UpdateRemainingTime();
            UpdateMover();

        }
        
        private void OnTriggerEnter(Collider other)
        {
            
            if(other.tag == "Booster")
            {

                ResetTimer();
                Destroy(other.gameObject);

            }

        }

        void ResetTimer()
        {

            remainingTime = maxBoostTime;

        }

        void SetMover()
        {
            mover = GetComponent<Mover>();
        }

        void GetMoverNormalSpeed()
        {
            baseSpeed = mover.GetSpeed();
        }

        private void ResetMoverSpeed()
        {

            mover.SetSpeed(baseSpeed);

        }

        void BoostMover()
        {

            mover.SetSpeed(baseSpeed * boostSpeedMultiplier);

        }

        void UpdateRemainingTime()
        {

            remainingTime -= Time.deltaTime;

        }

        private void UpdateMover()
        {

            if (remainingTime == 0)
                ResetMoverSpeed();
            else
                BoostMover();

        }

    }

}
