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
        // Did user click the left mouse button
        if(Input.GetMouseButton(0))
        {
            MoveToCursor();
        }

        UpdateAnimator();

    }



    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        
        bool hasHit = Physics.Raycast(ray, out hit);


        // If we hit something with our click then send the player
        if(hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
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
