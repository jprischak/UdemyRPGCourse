using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        /**
        *  VARIABLES
        * */
        // Constant


        // Public


        // Serialized
        [SerializeField] Weapon weapon = null;

        // Private




        /**
        *	CORE FUNCTIONS
        * */
        private void OnTriggerEnter(Collider other)
        {
            // If the player gameobject is what collided with us. Equip the weapon to 
            // the player and destroy the gameobject
            if(other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }



        /**
         *  CLASS FUNCTIONS
         * */
    }
}
