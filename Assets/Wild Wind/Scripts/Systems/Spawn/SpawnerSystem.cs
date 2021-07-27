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

namespace WildWind.Systems.Spawn
{

    public class SpawnerSystem : MonoSingleton<SpawnerSystem>
    {

        public int maxActiveMissiles;
        public int maxActivePowerups;
        public int maxActiveStars;

        private int activeMissiles = 0;
        private int activePowerups = 0;
        private int activeStars = 0;

        public float spawnDistance = 100;

        private List<GameObject> gameObjects = new List<GameObject>();

        public SpawnContainer missilesContainer;
        public SpawnContainer powerupsContainer;
        public SpawnContainer starsContainer;
        private PlayableDirector spawnDirector;

        public override void Awake()
        {

            base.Awake();
            MissileController.OnStartStatic += AddActiveMissiles;
            MissileController.OnDeathStatic += RemoveActiveMissiles;

            CollectibleBoost.OnStartStatic += AddActivePowerups;
            CollectibleBoost.OnDeathStatic += RemoveActivePowerups;

            CollectibleShield.OnStartStatic += AddActivePowerups;
            CollectibleShield.OnDeathStatic += RemoveActivePowerups;

            Star.OnStartStatic += AddActiveStars;
            Star.OnDeathStatic += RemoveActiveStars;

            PlayerController.OnDeathStatic += ClearObjects;
            PlayerController.OnDeathStatic += ResetSpawnDirector;

        }

        public override void Start()
        {

            base.Start();
            spawnDirector = GetComponent<PlayableDirector>();
            
        }

        public override void Update()
        {

            base.Update();

            UpdateSpawnDirector();

            if (GameSystem.Instance.player != null)
            {

                InstantiateObjects(missilesContainer, ref activeMissiles, ref maxActiveMissiles);
                InstantiateObjects(powerupsContainer, ref activePowerups, ref maxActivePowerups);
                InstantiateObjects(starsContainer, ref activeStars, ref maxActiveStars);

            }

        }

        private void InstantiateObjects(SpawnContainer spawnContainer, ref int activeRefObject, ref int maxActiveObjects)
        {

            if (activeRefObject < maxActiveObjects)
            {

                List<int> chance = new List<int>();

                foreach (SpawnObject b in spawnContainer.spawnObjects)
                {

                    if (chance.Count != 0)
                        chance.Add(chance[chance.Count - 1] + b.chance);
                    else
                        chance.Add(b.chance);

                }

                int rand = Random.Range(1, spawnContainer.overalChance);
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
                        gameObjects.Add(temp);
                        break;

                    }

                }

            }

        }

        private Vector3 RandomPosition(float randomAngle, Vector3 direction)
        {
            return new Vector3(Mathf.Cos(randomAngle) * direction.x - Mathf.Sin(randomAngle) * direction.z, 0, Mathf.Sin(randomAngle) * direction.x + Mathf.Cos(randomAngle) * direction.z);
        }

        private Transform GetPlayer()
        {
            return FindObjectOfType<PlayerController>().transform;
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

        public void AddActiveMissiles()
        {

            activeMissiles++;

        }

        public void RemoveActiveMissiles()
        {

            activeMissiles--;

        }

        public void AddActivePowerups()
        {

            activePowerups++;

        }

        public void RemoveActivePowerups()
        {

            activePowerups--;

        }

        public void AddActiveStars()
        {

            activeStars++;

        }

        public void RemoveActiveStars()
        {

            activeStars--;

        }

        public override void LateUpdate()
        {
            base.LateUpdate();
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

            foreach(GameObject a in gameObjects)
            {

                Destroy(a);

            }

            gameObjects.Clear();

        }

    }

}
