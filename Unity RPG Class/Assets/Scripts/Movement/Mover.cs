using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.CoreFeatures;




namespace RPG.Movement
{


    public class Mover : MonoBehaviour, IAction
    {
        /**
        *  VARIABLES
        * */
        // Constant



        // Public
        Ray lastRay;


        // Serialized
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6;


        // Private
        private NavMeshAgent navMeshAgent;
        private Health health;



        /**
        *	CLASS FUNCTIONS
        * */
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }


        private void Update()
        {
            // If player is dead disable nav mesh agent
            navMeshAgent.enabled = !health.IsDead();

            UpdateAnimator();
        }





        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }


        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }


        private void UpdateAnimator()
        {
            // This gets our velocity from the navMeshAgent
            Vector3 velocity = navMeshAgent.velocity;

            // This will convert the velocity from world to local
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            // Save our speed from our local velocity
            float speed = localVelocity.z;

            // Store our speed variable in the animator variable
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
            
        }


        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        

    }
}
