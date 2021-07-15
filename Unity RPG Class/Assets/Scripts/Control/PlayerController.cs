using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;


namespace RPG.Control
{
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
            if(InteracteWithCombat()) return;

            if(InteracteWithMovement()) return;

            print("Nothing to do");

        }




        
    
        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }


        private bool InteracteWithMovement()
        {
            RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);


            // If we hit something with our click then send the player
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().MoveTo(hit.point);
                }
                return true;
            }

            return false;
        }


        private bool InteracteWithCombat()
        {
            // Gets a list of all items that our mouse is looking at
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());


            // Loop through all items returned
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                // If our hit item doesnt have a combatTarget script attached to it then go to next item
                if (target == null) continue;

                // Check to see if we clicked
                if(Input.GetMouseButtonDown(0))
                {
                    // Call our attack function of our fighter script
                    GetComponent<Fighter>().Attack(target);
                }

                return true;
            }

            return false;

        }
    }
}
