using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        /**
        *  VARIABLES
        * */
        // Constant
        const float WAYPOINTGIZMORADIUS = 0.3f;

        // Public



        // Serialized
        
        

        // Private






        /**
        *  CLASS FUNCTIONS
        * */
        private void OnDrawGizmos() 
        {
            for(int i=0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);

                Gizmos.DrawSphere(GetWaypoint(i), WAYPOINTGIZMORADIUS);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        



        /**
        *  FUNCTIONS
        * */
        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }


        public int GetNextIndex(int i)
        {
            if(i + 1 == transform.childCount)
                return 0;
            else 
                return i + 1;

        }



    }
}
