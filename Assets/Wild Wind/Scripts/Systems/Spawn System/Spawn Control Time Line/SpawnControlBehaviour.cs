using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace WildWind.Systems.Spawn
{

    [Serializable]
    public class SpawnControlBehaviour : PlayableBehaviour
    {

        public int maxActiveMissiles;
        public int maxActivePowerups;
        public int maxActiveStars;

    }

}
