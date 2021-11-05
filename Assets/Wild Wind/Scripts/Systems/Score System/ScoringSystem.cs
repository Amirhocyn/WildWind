using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WildWind.Control;
using UnityFx.Async;
using UnityEngine.UI;

namespace WildWind.Systems
{

    public class ScoringSystem : MonoSingleton<ScoringSystem>
    {

        [SerializeField] int timeScore;
        public float scoreMultiplier = 1;
        [SerializeField]
        private Text scoreGUI;
        private int _score = 0;
        public int score
        {

            get
            {

                return _score;

            }
            private set
            {

                _score = value;

            }

        }
        Coroutine timer;

        public override void Start()
        {

            base.Start();

            PlayerController.OnStartStatic += StartTimer;
            PlayerController.OnDeathStatic += ResetScore;
            PlayerController.OnDeathStatic += StopTimer;

        }

        public override void Update()
        {

            base.Update();

        }

        public void ResetScore()
        {

            score = 0;

        }

        public void AddScore(int addScore)
        {

            score += (int)(addScore * scoreMultiplier);

        }

        public void StartTimer()
        {

            timer = StartCoroutine("Timer",timer);

        }

        public void StopTimer()
        {

            StopCoroutine(timer);

        }

        public IEnumerator Timer()
        {

            while(true)
            {

                yield return new WaitForSeconds(1);
                AddScore(timeScore);

            }

        }

    }

}
