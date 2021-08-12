using System.Collections.Generic;
using UnityEngine;
using System;
using UnityFx.Async;
using Random = UnityEngine.Random;

namespace WildWind.Systems.Spawn
{

    [CreateAssetMenu(fileName = "New Spawn Container",menuName = "Spawn Container",order = 0)]
    [Serializable]
    public class SpawnContainer : ScriptableObject
    {

        [SerializeField]
        public List<SpawnObject> spawnObjects = new List<SpawnObject>();
        public bool canAdd = true;

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

        public int maxActiveObjects;
        public Vector2 timeRange;
        public int activeObjects = 0;
        public int limitCap;

        private void OnValidate()
        {
            limitCap = maxActiveObjects;
        }

        public void IncreaseObjectCount()
        {

            activeObjects++;

        }

        public void DecreaseObjectCount()
        {

            activeObjects--;
            limitCap--;
            if (activeObjects < 0)
                activeObjects = 0;

        }

        public bool CanAddObject()
        {

            return activeObjects < limitCap;

        }

        public void Initialize()
        {

            TimerDelay();
            ResetValues();

        }

        public void ResetValues()
        {

            limitCap = maxActiveObjects;
            activeObjects = 0;

        }

        public async void TimerDelay()
        {

            while (GameSystem.Instance.IsPlaying())
            {

                if (limitCap != maxActiveObjects)
                {

                    await AsyncResult.Delay(Random.Range(timeRange.x, timeRange.y));
                    limitCap = Mathf.Clamp(limitCap + 1, 0, maxActiveObjects);

                }
                else
                    await AsyncResult.Delay(0.1f);

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
