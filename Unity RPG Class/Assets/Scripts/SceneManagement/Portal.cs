using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        /**
        *  VARIABLES
        * */
        // Constant


        // Public


        // Serialized
        [SerializeField] int sceneToLoad = -1;


        // Private




        /**
        *	CORE FUNCTIONS
        * */
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }



        /**
         *  CLASS FUNCTIONS
         * */
    }

}
