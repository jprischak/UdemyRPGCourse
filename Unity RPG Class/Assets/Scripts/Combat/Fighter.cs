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


        // Private
        private Transform targetTransform;





        /**
        *  CLASS FUNCTIONS
        * */
        


        private void Update()
        {
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


        private void AttackBehaviour()
        {
            GetComponent<Animator>().SetTrigger("attack");
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


        // Called from animator
        public void Hit() 
        {

        }
          
      

    }
}
