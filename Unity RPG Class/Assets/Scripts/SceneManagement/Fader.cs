using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        /**
        *  VARIABLES
        * */
        // Constant


        // Public


        // Serialized
        


        // Private
        private CanvasGroup canvasGroup;



        /**
        *	CORE FUNCTIONS
        * */
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }



        /**
         *  CLASS FUNCTIONS
         * */
        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;

                yield return null;
            }
        }


        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                print("Alpha = " + canvasGroup.alpha);

                canvasGroup.alpha -= Time.deltaTime / time;

                yield return null;
            }
        }
    }
}
