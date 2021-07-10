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
		GetComponent<NavMeshAgent>().destination = target.position;
	}
}
