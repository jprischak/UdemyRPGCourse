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
        if(Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }

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
}
