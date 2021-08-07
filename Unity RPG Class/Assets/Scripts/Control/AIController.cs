using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.CoreFeatures;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        /**
        *  VARIABLES
        * */
        // Constant



        // Public



        // Serialized
        [SerializeField] float chaseDistance = 5f;
        


        // Private
        private Fighter fighter;
        private GameObject player;
        private Health health;






        /**
        *  CLASS FUNCTIONS
        * */
        private void Start() 
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
        }


        private void Update()
        {
            // Check to see if we are dead
            if(health.IsDead()) return;


            if(InAttackRange(player) && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }

        }




        private bool InAttackRange(GameObject player)
        {
            // Check to see how far we are from the player
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return  distanceToPlayer < chaseDistance;
        }
    }
}

