using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Constant



    // Public



    // Serialized



    // Private






    /**
    *  CLASS FUNCTIONS
    * */
    private void Start()
    {

    }


    private void Update()
    {
        // Did user click the left mouse button
        if (Input.GetMouseButton(0))
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
        if (hasHit)
        {
            GetComponent<Mover>().MoveTo(hit.point);
        }
    }
}
