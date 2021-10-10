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
        [SerializeField] float fadeInTime = 0.2f;

        // Private




        /**
        *	CORE FUNCTIONS
        * */
        IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(DEFAULTSAVEFILE);
            yield return fader.FadeIn(fadeInTime);
            
        }


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
