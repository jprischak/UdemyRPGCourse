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
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        


        // Private
        private Fighter fighter;
        private Mover mover;
        private GameObject player;
        private Health health;
        private Vector3 gaurdLocation;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private int currentWaypointIndex = 0;
        





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
                // Reset the time since we last saw the enemy
                timeSinceLastSawPlayer = 0;

                AttackBehaviour();
            }
            else if(timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();

            }
            else
            {
                PatrolBehaviour();
            }



            // Update the time since we last saw the player
            timeSinceLastSawPlayer += Time.deltaTime;

        }

        

        private void OnDrawGizmosSelected()    
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);

        }




        /*
        *   FUNCTIONS
        */

        private bool InAttackRange(GameObject player)
        {
            // Check to see how far we are from the player
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return  distanceToPlayer < chaseDistance;
        }
    
    
        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelAction();
        }


        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = gaurdLocation;


            if(patrolPath != null)
            {
                if(AtWaypoint())
                {
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }

            mover.StartMoveAction(nextPosition);
        }



        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());

            return distanceToWaypoint < waypointTolerance;
        }


        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

    
    
    
    }
}

