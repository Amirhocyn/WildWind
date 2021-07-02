using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WildWind.Core
{

    public class Star : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Player")
                Destroy(gameObject);

        }

    }

}
