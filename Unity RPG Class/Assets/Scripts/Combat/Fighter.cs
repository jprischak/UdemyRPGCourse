using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.CoreFeatures;
using RPG.Movement;



namespace RPG.Combat
{


    public class Fighter : MonoBehaviour, IAction
    {
        /**
        *  VARIABLES
         */
        // Constant





        // Public
        public Weapon currentWeapon = null;
        public Health targetHealth;


        // Serialized
        [SerializeField] float      timeBetweenAttacks  = 1f;
        [SerializeField] Transform  rightHandTransform  = null;
        [SerializeField] Transform  leftHandTransform   = null;
        [SerializeField] Weapon     defaultWeapon       = null;


        // Private
        private float   timeSinceLastAttack     = Mathf.Infinity;
        




        /**
        *  UNITY FUNCTIONS
        * */
        private void Start()
        {
            EquipWeapon(defaultWeapon);
        }


        private void Update()
        {
            // Update our time since last attack
            timeSinceLastAttack += Time.deltaTime;


            // If we dont have a target exit to prevent calling the stop function when trying to move
            if(targetHealth == null) return;


            // If our target is already dead quick attacking them
            if(targetHealth.IsDead()) return;




            if(targetHealth != null && !GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(targetHealth.transform.position);
            }
            else 
            { 
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            } 
        }




        /**
        *   CLASS FUNCTIONS
        */
        private void AttackBehaviour()
        {

            // Make sure that we are looking at our target
            transform.LookAt(targetHealth.transform);

            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                // Reset our time since last attack
                timeSinceLastAttack = 0;
            }

        }

        private void TriggerAttack()
        {
            // Trigger animation
            GetComponent<Animator>().ResetTrigger("stopAttacking");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return (Vector3.Distance(transform.position, targetHealth.transform.position) < currentWeapon.GetWeaponRange());
        }

        private void StopAttack()
        {
            GetComponent<Animator>().SetTrigger("stopAttacking");
            GetComponent<Animator>().ResetTrigger("attack");
        }




        // Called from the animator
        public void Hit() 
        {
            if(targetHealth == null)
                return;

            if(currentWeapon.HasProjectile())
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, targetHealth);
           
            else
                targetHealth.TakeDamage(currentWeapon.GetDamage());


        }   

        // Called from the animator
        public void Shoot()
        {
            Hit();
        }


        public bool CanAttack(GameObject combatTarget)
        {
            // If our hit item doesnt have a combatTarget script attached to it then go to next item
            if (combatTarget == null) 
                return false;

            Health targetToTest = combatTarget.GetComponent<Health>();

            return targetToTest != null && !targetToTest.IsDead();
        }


        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            targetHealth = combatTarget.GetComponent<Health>();
        }


        public void Cancel()
        {
            StopAttack();
            targetHealth = null;
        }


        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;

            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

    }
}
