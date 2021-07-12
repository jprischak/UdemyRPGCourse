using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;







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


	/**
	*	CLASS FUNCTIONS
	* */
	private void Start()
	{
	}




	private void Update()
	{
        
        UpdateAnimator();

    }



   


    public void MoveTo(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().destination = destination;
    }


    private void UpdateAnimator()
    {
        // This gets our velocity from the navMeshAgent
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;

        // This will convert the velocity from world to local
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        // Save our speed from our local velocity
        float speed = localVelocity.z;

        // Store our speed variable in the animator variable
        GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
    }
}
