using UnityEngine;

namespace WildWind.Combat
{

    public class MissileCombat : Combat
    {

        private string playerTag = "Player";
        private string missileTag = "Missile";

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(playerTag) || other.CompareTag(missileTag))
                Destroy(gameObject);

        }

    }

}