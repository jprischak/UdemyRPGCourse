using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.CoreFeatures;

namespace RPG.Combat
{


    public class Projectile : MonoBehaviour
    {
        /**
        *  VARIABLES
        * */
        // Constant


        // Public


        // Serialized
        
        [SerializeField] float speed = 1f;


        // Private
        private Health target = null;



        /**
        *	CORE FUNCTIONS
        * */
        public void Update()
        {
            if (target == null)
                return;

            transform.LookAt(GetAimLocation());

            transform.Translate(transform.forward * speed * Time.deltaTime);
        }



        /**
         *  CLASS FUNCTIONS
         * */
        public Vector3 GetAimLocation()
        {
            CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();

            if (targetCollider == null)
                return target.transform.position;

            return target.transform.position + Vector3.up * targetCollider.height / 2;
        }

        
        public void SetTarget(Health newTarget)
        {
            target = newTarget;
        }
    }
}
