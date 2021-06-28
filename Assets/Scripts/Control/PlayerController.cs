using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WildWind.Movement;

namespace WildWind.Control
{

    public class PlayerController : MonoBehaviour
    {

        Mover mover;

        private void Start()
        {

            SetMover();

        }

        private void Update()
        {

            if ((Input.mousePosition.x > Screen.width / 2 && Input.GetMouseButton(0)) || Input.GetKey(KeyCode.RightArrow))
                mover.Turn(1);
            if ((Input.mousePosition.x < Screen.width / 2 && Input.GetMouseButton(0)) || Input.GetKey(KeyCode.LeftArrow))
                mover.Turn(-1);

        }

        private void SetMover()
        {

            mover = GetComponent<Mover>();

        }

    }

}
