using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.CoreFeatures;
using RPG.Movement;


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
        private Mover mover;
        private GameObject player;
        private Health health;

        private Vector3 gaurdLocation;





        /**
        *  CLASS FUNCTIONS
        * */
        private void Start() 
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            gaurdLocation = transform.position;
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
                mover.StartMoveAction(gaurdLocation);
            }

        }

        private void OnDrawGizmosSelected()    
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);

        }




        private bool InAttackRange(GameObject player)
        {
            // Check to see how far we are from the player
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return  distanceToPlayer < chaseDistance;
        }
    }
}

