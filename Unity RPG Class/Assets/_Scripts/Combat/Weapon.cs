using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.CoreFeatures;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]

    public class Weapon : ScriptableObject
    {

        /**
        *  VARIABLES
         */
        // Constant
        const string WEAPONENAME = "Weapon";




        // Public



        // Serialized
        [SerializeField] AnimatorOverrideController animatorOverride    = null;
        [SerializeField] GameObject                 equipedPrefab       = null;
        [SerializeField] float                      weaponDamage        = 5f;
        [SerializeField] float                      weaponRange         = 2f;
        [SerializeField] bool                       isRightHanded       = true;
        [SerializeField] Projectile                 projectile          = null;




        /**
        *   CLASS FUNCTIONS
        */
        // Figure out if weapon is right hand or left hand
        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            if (isRightHanded)
                return rightHand;
            else
                return leftHand;
        }


        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if(equipedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);

                // Inistiate weapon in correct hand
                GameObject weapon = Instantiate(equipedPrefab, handTransform);
                weapon.name = WEAPONENAME;
            }
               
                
            
            
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


        public bool HasProjectile()
        {
            return projectile != null;
        }


        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Transform handTransform = GetTransform(rightHand, leftHand);

            Projectile projectileInstance = Instantiate(projectile, handTransform.position, Quaternion.identity);

            projectileInstance.SetTarget(target, weaponDamage);
        }


        // Check both hands to see if we all ready have a weapon. If we do then destroy it, else just return
        // Change the name of the old weapon before destroying to make sure we don't destroy the wrong weapon
        public void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {

            Transform oldWeapon = rightHand.Find(WEAPONENAME);

            if (oldWeapon == null)
                oldWeapon = leftHand.Find(WEAPONENAME);

            if (oldWeapon == null)
                return;


            oldWeapon.name = "Destroying";
            Destroy(oldWeapon.gameObject);
        }


    }
}
