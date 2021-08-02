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
        private IAction currentAction;





        /**
        *  CLASS FUNCTIONS
        * */
        public void StartAction(IAction action) 
        {
            // If our current action is the same as last action then don't do anything
            if(currentAction == action) return;


            if(currentAction != null)
                currentAction.Cancel();
                
    
            currentAction = action;
           
        }
    }
}
