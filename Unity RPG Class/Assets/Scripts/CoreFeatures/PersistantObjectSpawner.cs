using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.CoreFeatures
{
    public class PersistantObjectSpawner : MonoBehaviour
    {
        /**
        *  VARIABLES
        * */
        // Constant


        // Public
        public static bool hasSpawned = false;


        // Serialized
        [SerializeField] GameObject persistentObjectPrefab;


        // Private
        



        /**
        *	CORE FUNCTIONS
        * */
        private void Awake()
        {
            if (hasSpawned) return;

            SpawnPersistentObjects();

            hasSpawned = true;

        }




        /**
         *  CLASS FUNCTIONS
         * */
        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }

    }
}
