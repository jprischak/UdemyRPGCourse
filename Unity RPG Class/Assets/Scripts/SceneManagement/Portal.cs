using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.CoreFeatures;
using RPG.Saving;



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
        [SerializeField] int sceneToLoad        = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime      = 1f;
        [SerializeField] float fadeInTime       = 3f;
        [SerializeField] float fadeWaitTime     = 0.5f;


        // Private
        enum DestinationIdentifier
        {
            A, B, C, D
        }





        /**
        *	CORE FUNCTIONS
        * */
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }



        /**
         *  CLASS FUNCTIONS
         * */
        private IEnumerator Transition()
        {
            if(sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set.");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();


            yield return fader.FadeOut(fadeOutTime);

            // Save current level
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            // Load current level
            wrapper.Load();


            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            // Once we have got our self setup in the new level save our state
            wrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);


            Destroy(gameObject);
        }


        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.transform.position;
            player.transform.rotation = otherPortal.spawnPoint.transform.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;

        }


        private Portal GetOtherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                
                return portal;
            }

            return null;
        }

    }

}
