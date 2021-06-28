using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WildWind.Control;

public class CameraController : MonoBehaviour
{

    private void Start()
    {

        GameObject followTarget = GameObject.FindGameObjectWithTag("Player");

        if (followTarget != null)
            GetComponent<CinemachineVirtualCamera>().Follow = followTarget.transform;

    }

}
