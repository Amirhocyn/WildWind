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

    public class PlayerController : MonoBehaviourMaster<PlayerController>, IBuyable
    {

        Mover mover;

        [SerializeField]private int _price;
        public int price { get { return _price; } set { _price = value; } }

        public override void Start()
        {

            base.Start();
            SetMover();

        }

        public void Update()
        {

            if (Application.platform == RuntimePlatform.WindowsEditor)
            {

                mover.Turn(0);

                if ((Input.mousePosition.x > Screen.width / 2 && Input.GetMouseButton(0)) || Input.GetKey(KeyCode.RightArrow))
                    mover.Turn(1);
                if ((Input.mousePosition.x < Screen.width / 2 && Input.GetMouseButton(0)) || Input.GetKey(KeyCode.LeftArrow))
                    mover.Turn(-1);

            }

            if (Application.platform == RuntimePlatform.Android)
            {

                mover.Turn(0);
                if (Input.touchCount == 0)
                    return;

                if (Input.GetTouch(0).position.x > Screen.width / 2)
                    mover.Turn(1);
                if (Input.GetTouch(0).position.x < Screen.width / 2)
                    mover.Turn(-1);

            }

        }

        private void SetMover()
        {

            mover = GetComponent<Mover>();

        }

        public override void OnDestroy()
        {

            if (base.OnDeath != null)
                base.OnDestroy();

        }

        public bool Buy(ref int balance)
        {

            bool canBuy = balance >= price;
            if (canBuy)
                balance -= price;
            return canBuy;

        }
    }

}
