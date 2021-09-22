using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;


namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        /**
        *  VARIABLES
        * */
        // Constant
        const string DEFAULTSAVEFILE = "save";

        // Public


        // Serialized


        // Private




        /**
        *	CORE FUNCTIONS
        * */
        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }



        /**
         *  CLASS FUNCTIONS
         * */
        public void Load()
        {
            GetComponent<SavingSystem>().Load(DEFAULTSAVEFILE);
            print("Loading!");
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(DEFAULTSAVEFILE);
        }
    }
}
