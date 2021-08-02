using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.CoreFeatures
{

    public class FollowCamera : MonoBehaviour
    {

        /**
        *  VARIABLES
        * */
        // Constant



        // Public



        // Serialized
        [SerializeField] Transform target;


        // Private






        /**
        *  CLASS FUNCTIONS
        * */

        // Start is called before the first frame update
        void Start()
        {

        }



        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}
