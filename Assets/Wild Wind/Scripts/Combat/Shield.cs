using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WildWind.Combat
{

    public class Shield : MonoBehaviourMaster<Shield>
    {
        [SerializeField]Image shieldImg;
        private int _shields = 0;
        int shields
        {

            get
            {

                return _shields;

            }

            set
            {

                _shields = Mathf.Clamp(value, 0,20);
                InteractWithCombat();
                if (_shields != 0)
                    shieldImg.gameObject.SetActive(true);
                else
                    shieldImg.gameObject.SetActive(false);

            }

        }

        public override void Start()
        {
            base.Start();
        }

        private void OnTriggerEnter(Collider other)
        {

            InteractWithObject(other.gameObject);

        }

        public override void OnDestroy()
        {
            base.OnDestroy();
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
