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
        [SerializeField] GameObject weaponPrefab = null;




        /**
        *   CLASS FUNCTIONS
        */
        public void Spawn(Transform handTransform, Animator animator)
        {
            Instantiate(weaponPrefab, handTransform);
            animator.runtimeAnimatorController = animatorOverride;
        }
    }
}
