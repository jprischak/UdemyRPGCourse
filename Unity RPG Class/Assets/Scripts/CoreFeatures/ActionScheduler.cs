using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.CoreFeatures
{
    public class ActionScheduler : MonoBehaviour
    {
        /**
        *  VARIABLES
            */
        // Constant



        // Public



        // Serialized



        // Private
        private MonoBehaviour currentAction;





        /**
        *  CLASS FUNCTIONS
        * */
        public void StartAction(MonoBehaviour action) 
        {
            // If our current action is the same as last action then don't do anything
            if(currentAction == action) return;


            if(currentAction != null)
                print("Cancelling " + action);
                
    
            currentAction = action;
           
        }
    }
}
