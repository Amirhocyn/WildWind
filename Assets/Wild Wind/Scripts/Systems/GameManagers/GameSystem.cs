using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityFx.Async;
using WildWind.Control;
using WildWind.Core;
using Random = UnityEngine.Random;

namespace WildWind.Systems
{

    public class GameSystem : MonoSingleton<GameSystem>
    {

        public PlayerController player;
        public CameraController cameraController;
        private bool isPlaying = false;
        public enum GameState { stop,playing}
        GameState gameState;

        public Action Reset;

        [SerializeField]
        PlayerController[] planes;

        public override void Awake()
        {

            base.Awake();

        }

        public override void Start()
        {

            base.Start();

            //PlayerController.OnStartStatic += StartPlaying;
            //PlayerController.OnDeathStatic += StopPlaying;
            PlayerController.OnDeathStatic += InstantiatePlayer;

            InstantiatePlayer();
            
            //Reset += InstantiatePlayer;
            //Reset += SetupCamera;

        }

        private void SetupCamera()
        {
            print("here again");
            cameraController.SetFollowTarget(player.gameObject);
        }

        private async void InstantiatePlayer()
        {

            await AsyncResult.Delay(1000);
            int rand = Random.Range(0, planes.Length);
            player = Instantiate(planes[rand], Vector3.zero, Quaternion.identity);
            SetupCamera();

        }

        public override void Update()
        {
            base.Update();
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

        public void StartPlaying()
        {

            isPlaying = true;

        }

        public void StopPlaying()
        {

            isPlaying = false;

        }

        public bool IsPlaying()
        {

            return isPlaying;

        }

    }

}
