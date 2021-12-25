using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{

    private void Start()
    {

        print(JsonUtility.ToJson(new data()));

    }

    private void Update()
    {
        
        

    }

    private void LateUpdate()
    {

        

    }

}

[Serializable]
public class data
{

    [SerializeField]int a = 0;

}
