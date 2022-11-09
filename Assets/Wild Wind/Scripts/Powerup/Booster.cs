using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using WildWind.Control;
using WildWind.Core;
using WildWind.Movement;

namespace WildWind.Powerup
{

    public class Booster : MonoBehaviour
    {

        [SerializeField] int boosterTime = 5;
        [SerializeField] float boosterSpeedMultiplication = 1.5f;

        private bool isConsumed = false;
        private const string playerTag = "Player";
        private static CancellationTokenSource cancellationTokenSource;
        private static Task boosterTask;

        private async void OnTriggerEnter(Collider other)
        {
            if (!isConsumed && other.CompareTag(playerTag))
            {

                isConsumed = true;
                Destroy(gameObject);
                PlayerController playerController = other.GetComponentInParent<PlayerController>();

                if (cancellationTokenSource != null) cancellationTokenSource.Cancel();

                if (boosterTask != null)
                {
                    await boosterTask;
                    cancellationTokenSource.Dispose();
                }

                ApplyBooster(playerController);

            }
        }

        private void ApplyBooster(PlayerController playerController)
        {

            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            float initTime = WildWindTime.Instance.time;

            MoverData moverData = ScriptableObject.CreateInstance<MoverData>();
            CreateBoosterMoverData(moverData, playerController);

            Func<bool> violationCondition = (() =>
            {
                return boosterTime + initTime < WildWindTime.Instance.time;
            });

            new ObjectSwaper<MoverData>(() => { return ref playerController.moverData; }, () => { return ref moverData; }, violationCondition, cancellationTokenSource, ref boosterTask);
        }

        private void CreateBoosterMoverData(MoverData moverData,PlayerController playerController)
        {
            moverData.speed = playerController.GetMoverData().speed * boosterSpeedMultiplication;
            moverData.yawRate = playerController.GetMoverData().yawRate;
            moverData.rollRate = playerController.GetMoverData().rollRate;
        }

    }

}