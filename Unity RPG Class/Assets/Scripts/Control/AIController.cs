using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;


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
        


        // Private
        private Fighter fighter;
        private GameObject player;






        /**
        *  CLASS FUNCTIONS
        * */
        private void Start() 
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }


        private void Update()
        {
            if(InAttackRange(player) && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }

        }




        private bool InAttackRange(GameObject player)
        {
            // Check to see how far we are from the player
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return  distanceToPlayer < chaseDistance;
        }
    }
}

