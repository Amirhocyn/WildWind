using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WildWind.Control;
using UnityFx.Async;

namespace WildWind.Systems
{

    public class ScoringSystem : MonoSingleton<ScoringSystem>
    {

        [SerializeField] int timeScore;
        public float scoreMultiplier = 1;
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
        public bool isPlaying;

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
            print(score);

        }

        public void StartTimer()
        {

            isPlaying = true;
            Timer();

        }

        public void StopTimer()
        {

            isPlaying = false;

        }

        public async void Timer()
        {

            while (isPlaying)
            {

                await AsyncResult.Delay(1000);
                if (isPlaying)
                    AddScore(timeScore);

            }

        }

    }

}
