using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.CoreFeatures;
using RPG.Combat;




namespace RPG.Movement
{


    public class Mover : MonoBehaviour
    {
        /**
        *  VARIABLES
        * */
        // Constant



        // Public
        Ray lastRay;


        // Serialized
        [SerializeField] Transform target;


        // Private
        private NavMeshAgent navMeshAgent;



        /**
        *	CLASS FUNCTIONS
        * */
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }


        private void Update()
        {
            UpdateAnimator();
        }





        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }


        public void Stop()
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
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }


        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }


    }
}
