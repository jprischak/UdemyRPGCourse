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


        // Private
        private Transform targetTransform;
        private float timeSinceLastAttack;




        /**
        *  UNITY FUNCTIONS
        * */
        


        private void Update()
        {
            // Update our time since last attack
            timeSinceLastAttack += Time.deltaTime;


            // If we dont have a target exit to prevent calling the stop function when trying to move
            if(targetTransform == null) return;


            if(targetTransform != null && !GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(targetTransform.position);
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
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                // Trigger animation
                GetComponent<Animator>().SetTrigger("attack");

                // Reset our time since last attack
                timeSinceLastAttack = 0;
            }
            
        }


        // Called from animator
        public void Hit() 
        {
            Health healthComponent = targetTransform.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
        }



        private bool GetIsInRange()
        {   
            return (Vector3.Distance(transform.position, targetTransform.position) < weaponRange);
        }


        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            targetTransform = combatTarget.transform;
        }


        public void Cancel()
        {
            targetTransform = null;
        }
    }
}
