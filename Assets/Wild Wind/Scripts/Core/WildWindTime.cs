using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WildWind.Systems;

namespace WildWind.Core
{

    public class WildWindTime : MonoSingleton<WildWindTime>
    {

        public float time = 0;
        public float deltaTime = 0;
        public float timeScale;

        private void Update()
        {

            time = Time.time;
            deltaTime = Time.deltaTime;
            timeScale = Time.timeScale;

        }

    }

}
