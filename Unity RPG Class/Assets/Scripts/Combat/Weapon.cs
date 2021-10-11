using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]

    public class Weapon : ScriptableObject
    {

        /**
        *  VARIABLES
         */
        // Constant





        // Public



        // Serialized
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equipedPrefab = null;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float weaponRange = 2f;




        /**
        *   CLASS FUNCTIONS
        */
        public void Spawn(Transform handTransform, Animator animator)
        {
            if(equipedPrefab != null)
                
                Instantiate(equipedPrefab, handTransform);
                
            
            
            if(animatorOverride != null)
                animator.runtimeAnimatorController = animatorOverride;

        }


        public float GetDamage()
        {
            return weaponDamage;
        }


        public float GetWeaponRange()
        {
            return weaponRange;
        }
    }
}
