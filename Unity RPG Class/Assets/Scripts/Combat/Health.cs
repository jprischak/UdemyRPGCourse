using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    
    public class Health : MonoBehaviour
    {

        /**
        *  VARIABLES
        * */
        // Constant



        // Public



        // Serialized
        [SerializeField] float health = 100f;



        // Private






        /**
        *  CLASS FUNCTIONS
        * */
        public void TakeDamage(float damage)
        {
            // Make sure that our health doesn't go below 0
            health = Mathf.Max(health - damage, 0);

            Debug.Log("Enemy Health " + health);
        }
    }
}
