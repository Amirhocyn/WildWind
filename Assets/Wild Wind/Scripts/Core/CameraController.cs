using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WildWind.Control;


namespace WildWind.Core
{

    public class CameraController : MonoBehaviourMaster<CameraController>
    {

        public override void Start()
        {

            GameObject followTarget = GameObject.FindGameObjectWithTag("Player");

            if (followTarget != null)
                GetComponent<CinemachineVirtualCamera>().Follow = followTarget.transform;

        }

    }

}