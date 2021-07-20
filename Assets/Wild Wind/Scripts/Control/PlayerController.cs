using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WildWind.Movement;
using WildWind.Core;
using UnityEngine.Playables;

namespace WildWind.Control
{

    [System.Serializable]

    public class PlayerController : MonoBehaviourMaster<PlayerController>, INotificationReceiver
    {

        Mover mover;

        public void SayHi()
        {

            print("Hi");

        }

        public override void Start()
        {

            base.Start();
            SetMover();

        }

        public override void Update()
        {

            base.Update();
            if ((Input.mousePosition.x > Screen.width / 2 && Input.GetMouseButton(0)) || Input.GetKey(KeyCode.RightArrow))
                mover.Turn(1);
            if ((Input.mousePosition.x < Screen.width / 2 && Input.GetMouseButton(0)) || Input.GetKey(KeyCode.LeftArrow))
                mover.Turn(-1);

        }

        private void SetMover()
        {

            mover = GetComponent<Mover>();

        }

        public override void OnDestroy()
        {

            base.OnDestroy();

        }

        public void OnNotify(Playable origin, INotification notification, object context)
        {
            print(notification.ToString());
        }
    }

}
