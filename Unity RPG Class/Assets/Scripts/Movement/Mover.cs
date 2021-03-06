using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.CoreFeatures;
using RPG.Saving;




namespace RPG.Movement
{


    public class Mover : MonoBehaviour, IAction, ISaveable
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




        /*
         *  FUNCTIONS
         * */
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


        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }


        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }


        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }


        public object CaptureState()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            return data;
        }


        public void RestoreState(object state)
        {
            Dictionary<string, object> data = (Dictionary<string, object>)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = ((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
