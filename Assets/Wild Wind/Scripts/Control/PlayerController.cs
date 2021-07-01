using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WildWind.Movement;
using WildWind.Core;

namespace WildWind.Control
{

    [System.Serializable]

    public class PlayerController : MonoBehaviourMaster<PlayerController>
    {

        Mover mover;

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

    }

}
