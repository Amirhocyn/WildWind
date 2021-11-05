using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WildWind.Movement
{

    public class Boost : MonoBehaviourMaster<Boost>
    {

        [SerializeField] float maxBoostTime = 10;
        [SerializeField] float boostSpeedMultiplier;
        private float defaultSpeed;
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

            base.Start();
            SetMover();
            GetMoverDefaultSpeed();          

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

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        void ResetTimer()
        {

            remainingTime = maxBoostTime;

        }

        void SetMover()
        {
            mover = GetComponent<Mover>();
        }

        void GetMoverDefaultSpeed()
        {

            defaultSpeed = mover.GetSpeed();

        }

        private void ResetMoverSpeed()
        {

            mover.SetSpeed(defaultSpeed);

        }

        void BoostMover()
        {

            mover.SetSpeed(defaultSpeed * boostSpeedMultiplier);

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
