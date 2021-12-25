using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WildWind.Movement
{

    public class Boost : MonoBehaviourMaster<Boost>
    {
        [SerializeField] private float maxBoostTime = 10;
        [SerializeField] private float boostSpeedMultiplier;
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
        private const string boosterTag = "Booster";

        public override void Start()
        {

            base.Start();
            SetMover();
            GetMoverDefaultSpeed();          

        }

        public void Update()
        {

            if (remainingTime != 0)
            {
                UpdateRemainingTime();
                UpdateMover();
            }


        }
        
        private void OnTriggerEnter(Collider other)
        {
            
            if(other.CompareTag(boosterTag))
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
