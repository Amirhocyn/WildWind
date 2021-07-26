using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace WildWind.Systems.Spawn
{

    [CreateAssetMenu(fileName = "New Spawn Container",menuName = "Spawn Container",order = 0)]
    [Serializable]
    public class SpawnContainer : ScriptableObject
    {

        [SerializeField]
        public List<SpawnObject> spawnObjects = new List<SpawnObject>();

        public int overalChance
        {

            get
            {

                int chance = 0;
                foreach(SpawnObject a in spawnObjects)
                {

                    chance += a.chance;

                }

                return chance;

            }

        }

    }

    [Serializable]
    public class SpawnObject
    {

        [SerializeField]
        public GameObject gameObject;
        [SerializeField]
        [Range(0,100)]
        public int chance;

    }

}
