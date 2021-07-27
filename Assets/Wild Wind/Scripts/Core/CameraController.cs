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

            base.Start();

        }

        public void SetFollowTarget(GameObject followTarget)
        {

            GetComponent<CinemachineVirtualCamera>().Follow = followTarget.transform;

        }

    }

}
