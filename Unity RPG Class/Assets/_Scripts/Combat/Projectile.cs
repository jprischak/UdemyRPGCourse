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
        private Health  target = null;
        private float   damage = 0.0f;



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


        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target)
                return;

            target.TakeDamage(damage);

            Destroy(gameObject);
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

        
        public void SetTarget(Health newTarget, float newDamage)
        {
            target = newTarget;
            damage = newDamage;
        }


   
    }
}
