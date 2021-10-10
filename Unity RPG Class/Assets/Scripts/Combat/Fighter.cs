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



        // Serialized
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] Transform handTransform = null;


        // Private
        private Health targetHealth;
        private float timeSinceLastAttack = Mathf.Infinity;




        /**
        *  UNITY FUNCTIONS
        * */
        private void Start()
        {
            SpawnWeapon();
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
                GetComponent<Mover>().MoveTo(targetHealth.transform.position, 1f);
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


        // Called from animator
        public void Hit() 
        {
            if(targetHealth == null)
                return;

            targetHealth.TakeDamage(weaponDamage);
        }


        private bool GetIsInRange()
        {   
            return (Vector3.Distance(transform.position, targetHealth.transform.position) < weaponRange);
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

        private void StopAttack()
        {
            GetComponent<Animator>().SetTrigger("stopAttacking");
            GetComponent<Animator>().ResetTrigger("attack");
        }

        private void SpawnWeapon()
        {
            Instantiate(weaponPrefab, handTransform);
        }
    }
}
