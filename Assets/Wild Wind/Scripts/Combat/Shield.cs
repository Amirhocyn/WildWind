using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WildWind.Combat
{

    public class Shield : MonoBehaviourMaster<Shield>
    {

        private int _shields = 0;
        int shields
        {

            get
            {

                return _shields;

            }

            set
            {

                _shields = Mathf.Clamp(value, 0,3);

            }

        }

        public override void Update()
        {



        }

        private void OnTriggerEnter(Collider other)
        {

            InteractWithObject(other.gameObject);
            InteractWithCombat();

        }

        private void InteractWithObject(GameObject other)
        {

            if (other.tag == "Shield")
            {

                shields++;
                Destroy(other.gameObject);

            }

            if (other.tag == "Missile")
                shields--;

        }

        private void InteractWithCombat()
        {

            if (shields == 0)
                GetComponent<Combat>().SetDestructible(true);
            else
                GetComponent<Combat>().SetDestructible(false);

        }

    }

}
