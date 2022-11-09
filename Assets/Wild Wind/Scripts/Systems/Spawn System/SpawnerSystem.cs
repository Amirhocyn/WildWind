using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using WildWind.Combat;
using WildWind.Control;
using WildWind.Core;
using WildWind.Movement;
using WildWind.Systems;
using EventHandler = UnityEngine.EventHandler;
using Random = UnityEngine.Random;

namespace WildWind.Systems.Spawn
{

    public class SpawnerSystem : MonoSingleton<SpawnerSystem>
    {

        public int maxActiveMissiles;
        public int maxActivePowerups;
        public int maxActiveStars;

        public float spawnDistance = 100;

        private List<GameObject> spawnedObjects = new List<GameObject>();
        [SerializeField]
        private List<SpawnContainer> spawnContainers;
        private PlayableDirector spawnDirector;

        public override void Awake()
        {

            base.Awake();

            //PlayerController.OnDeathStatic += ClearObjects;
            GameSystem.Instance.OnGameStart += ClearObjects;
            PlayerController.OnDeathStatic += ResetSpawnDirector;

        }

        public override void Start()
        {

            base.Start();
            spawnDirector = GetComponent<PlayableDirector>();

            foreach (SpawnContainer a in spawnContainers)
            {

                PlayerController.OnStartStatic += a.Initialize;

            }

        }

        public void Update()
        {

            UpdateSpawnDirector();

            if (GameSystem.Instance.gameState == GameSystem.GameState.Playing)
            {

                for (int j = 0; j < spawnContainers.Count; j++)
                    InstantiateObjects(spawnContainers[j]);

            }

        }

        private void InstantiateObjects(SpawnContainer spawnContainer)
        {

            if (spawnContainer.CanAddObject())
            {

                List<int> chance = new List<int>();

                for (int j = 0;j < spawnContainer.spawnObjects.Count;j++)
                {

                    if (chance.Count != 0)
                        chance.Add(chance[chance.Count - 1] + spawnContainer.spawnObjects[j].chance);
                    else
                        chance.Add(spawnContainer.spawnObjects[j].chance);

                }

                int rand = Random.Range(0, spawnContainer.overalChance);
                float randAngle = RandomAngle();

                for (int j = 0; j < chance.Count; j++)
                {

                    if (rand <= chance[j])
                    {

                        Vector3 pos;
                        Transform playerTransform = GameSystem.Instance.player.transform;

                        pos = RandomPosition(randAngle, playerTransform.forward);
                        pos *= spawnDistance;
                        pos += playerTransform.position;

                        GameObject temp = Instantiate(spawnContainer.spawnObjects[j].gameObject, pos, Quaternion.identity);
                        if (temp.GetComponent<EventHandler>() != null)
                        {

                            temp.GetComponent<EventHandler>().OnStart += spawnContainer.IncreaseObjectCount;
                            temp.GetComponent<EventHandler>().OnDeath += spawnContainer.DecreaseObjectCount;

                        }
                        spawnedObjects.Add(temp);
                        break;

                    }

                }

            }

        }

        private Vector3 RandomPosition(float randomAngle, Vector3 direction)
        {
            return new Vector3(Mathf.Cos(randomAngle) * direction.x - Mathf.Sin(randomAngle) * direction.z, 0, Mathf.Sin(randomAngle) * direction.x + Mathf.Cos(randomAngle) * direction.z);
        }

        private float RandomAngle()
        {
            return Random.Range(0, Mathf.PI * 4);
        }

        private void UpdateSpawnDirector()
        {
            
            spawnDirector.time = Mathf.Clamp(ScoringSystem.Instance.score,0,(float)spawnDirector.duration - 1);

        }

        private void ResetSpawnDirector()
        {

            spawnDirector.time = 0;

        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public void ClearObjects()
        {

            foreach(GameObject a in spawnedObjects)
            {

                Destroy(a);

            }

            spawnedObjects.Clear();

        }

    }

}
