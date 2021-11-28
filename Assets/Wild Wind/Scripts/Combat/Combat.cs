using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WildWind.Combat
{

    public class Combat : MonoBehaviourMaster<Combat>
    {

        private bool destructible { get; set; }

        private void OnTriggerEnter(Collider other)
        {

            InteractWithObject(other.gameObject);

        }

        private void InteractWithObject(GameObject other)
        {

            if (destructible && ((other.tag == "Player" && this.tag != "Player") || other.tag == "Missile"))
            {

                Destroy(gameObject);

            }

        }

        public void SetDestructible(bool destructible)
        {

            this.destructible = destructible;

        }

    }

}
