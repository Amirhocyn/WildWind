using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WildWind.Systems
{

    public class ScoreMultiplier : MonoBehaviourMaster<ScoreMultiplier>
    {

        private float _remaningTime = 0;
        private float remainingTime 
        { 
            get 
            {

                return _remaningTime;
            
            }
            set 
            {

                _remaningTime = Mathf.Clamp(value, 0, maxMultplierTime);
            
            } 
        }

        [SerializeField] float maxMultplierTime;
        private int defaultScoreMultiplier = 1;

        public override void Awake()
        {

            base.Awake();

        }

        public override void Start()
        {

            base.Start();
            GetDefaultScoreMultiplier();

        }

        private void GetDefaultScoreMultiplier()
        {

            defaultScoreMultiplier = ScoringSystem.Instance.scoreMultiplier;

        }

        public void Update()
        {

            UpdateRemainingTime();
            UpdateScoreMultiplier();

        }

        private void UpdateScoreMultiplier()
        {

            if (remainingTime == 0)
                ScoringSystem.Instance.scoreMultiplier = defaultScoreMultiplier;
            else
            {

                ScoringSystem.Instance.scoreMultiplier = defaultScoreMultiplier * 2;

            }

        }

        public override void OnDestroy()
        {

            base.OnDestroy();

        }

        public override void OnEnable()
        {

            base.OnEnable();

        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Score Multiplier")
            {

                ResetTimer();
                Destroy(other.gameObject);

            }

        }

        private void ResetTimer()
        {

            remainingTime = maxMultplierTime;

        }

        void UpdateRemainingTime()
        {

            remainingTime -= Time.deltaTime;

        }

    }

}
