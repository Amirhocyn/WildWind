using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

        [SerializeField]
        PlayerController[] planes;

        public override void Awake()
        {

            base.Awake();

        }

        public override void Start()
        {

            base.Start();

            PlayerController.OnDeathStatic += SetupPlayer;
            PlayerController.OnStartStatic += StartPlaying;
            PlayerController.OnDeathStatic += StopPlaying;
            SetupPlayer();
            
        }

        private void SetPlayerAsAlertCenter()
        {

            Alert.alertCenter = player.transform;

        }

        private void SetupCamera()
        {
            cameraController.SetFollowTarget(player.gameObject);
        }

        private void SetupPlayer()
        {
            StartCoroutine(InstantiatePlayer());
        }

        private IEnumerator InstantiatePlayer()
        {

            yield return new WaitForSeconds(1f);
            int rand = Random.Range(0, planes.Length);
            player = Instantiate(planes[rand], Vector3.zero, Quaternion.identity);
            SetupCamera();
            SetPlayerAsAlertCenter();

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

        private void OnApplicationQuit()
        {

            StopPlaying();

        }

        private void OnApplicationFocus(bool focus)
        {

            StartPlaying();

        }

        private void OnApplicationPause(bool pause)
        {

            StopPlaying();

        }

    }

}
