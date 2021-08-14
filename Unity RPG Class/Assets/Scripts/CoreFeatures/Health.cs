using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.CoreFeatures
{
    
    public class Health : MonoBehaviour
    {

        /**
        *  VARIABLES
        * */
        // Constant



        // Public



        // Serialized
        [SerializeField] float healthPoints = 100f;
        


        // Private
        private bool isDead = false;






        /**
        *  CLASS FUNCTIONS
        * */
        private void Die()
        {
            if(!isDead)
            {
                isDead = true;
                GetComponent<Animator>().SetTrigger("die");
                GetComponent<ActionScheduler>().CancelAction();
            }
            
            
            
        }

        public void TakeDamage(float damage)
        {
            // Make sure that our health doesn't go below 0
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if(healthPoints == 0)
            {
                Die();
            }
        }


        public bool IsDead()
        {
            return isDead;
        }



     
    }
}
