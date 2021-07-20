using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WildWind.Core;
using AnimationCurve = WildWind.Core.AnimationCurve;

namespace WildWind.Systems.SpawnSystem
{

    [CreateAssetMenu(fileName = "New Spawn Component",menuName ="Spawn Component",order = 0)]
    public class SpawnComponent : ScriptableObject
    {

        [SerializeField]GameObject objectToSpawn;
        [SerializeField]AnimationCurve spawnTimeFrame;
        [SerializeField]int spawnInterval = 10;
        private bool availability = true;

    }

}
