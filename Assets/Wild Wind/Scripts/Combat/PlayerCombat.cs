using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WildWind.Combat
{

    public class PlayerCombat : Combat
    {

        [SerializeField] Image shieldImg;
        private int _shields = 0;
        int shields
        {

            get
            {

                return _shields;

            }
            set
            {

                _shields = Mathf.Clamp(value, 0, 4);
                shieldImg.gameObject.SetActive(shields >= 1);

            }

        }

        private bool isDestructible { get => shields == 0; }

        private string missileTag = "Missile";

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(missileTag))
            {
                shields--;
                if (isDestructible)
                    Destroy(gameObject);
            }

        }

    }

}