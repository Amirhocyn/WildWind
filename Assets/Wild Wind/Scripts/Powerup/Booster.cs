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

        [SerializeField] float boosterTime = 5;
        [SerializeField] float boosterMultiplication = 1.5f;

        private bool isConsumed = false;
        private MoverData moverData;
        private string playerTag = "Player";
        private static CancellationTokenSource CancellationTokenSource;
        private static Task boosterTask;

        private async void OnTriggerEnter(Collider other)
        {
            if (!isConsumed && other.CompareTag(playerTag))
            {
                isConsumed = true;
                Destroy(gameObject);
                moverData = ScriptableObject.CreateInstance<MoverData>();

                PlayerController playerController = other.GetComponentInParent<PlayerController>();

                if (CancellationTokenSource != null) CancellationTokenSource.Cancel();

                if (boosterTask != null)
                {
                    await boosterTask;
                    CancellationTokenSource.Dispose();
                }

                ApplyBooster(playerController);
            }
        }

        private void ApplyBooster(PlayerController playerController)
        {

            CancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = CancellationTokenSource.Token;

            float initTime = WildWindTime.Instance.time;

            CreateBoosterMoverData(playerController);

            Func<bool> violationCondition = (() =>
            {
                return boosterTime + initTime < WildWindTime.Instance.time;
            });

            new ObjectSwaper<MoverData>(() => { return ref playerController.moverData; }, () => { return ref moverData; }, violationCondition, CancellationTokenSource, ref boosterTask);
        }

        private void CreateBoosterMoverData(PlayerController playerController)
        {
            moverData.speed = playerController.GetMoverData().speed * boosterMultiplication;
            moverData.yawRate = playerController.GetMoverData().yawRate;
            moverData.rollRate = playerController.GetMoverData().rollRate;
        }

    }

}