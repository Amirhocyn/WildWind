using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WildWind.Core
{

    public class Star : MonoBehaviourMaster<Star>
    {
        public override void Awake()
        {
            base.Awake();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override void Start()
        {
            base.Start();
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Player")
                Destroy(gameObject);

        }

    }

}
