using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("jere");
        if (other.CompareTag("Player"))
            Debug.Log(other.name + "    " + other.tag);
    }

}
