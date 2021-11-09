using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterController : MonoBehaviour
{
    /**
    *  VARIABLES
    * */
    // Constant


    // Public


    // Serialized
    [SerializeField] float acceleration         = 0.2f;
    [SerializeField] float decceleration        = 0.2f;
    [SerializeField] float maximumWalkVelocity  = 0.5f;
    [SerializeField] float maximumRunVelocity   = 2.0f;

    // Private
    private Animator    animator;
    private float       velocityX = 0.0f;
    private float       velocityZ = 0.0f;
    private int         velocityZHash;
    private int         velocityXHash;
    



    /**
    *	CORE FUNCTIONS
    * */
    public void Start()
    {
        animator = GetComponent<Animator>();


        // Save a key to the animator variable
        velocityXHash = Animator.StringToHash("VelocityX");
        velocityZHash = Animator.StringToHash("VelocityZ");
    }


    public void Update()
    {
        // Check input from the player
        bool forwardPressed     = Input.GetKey(KeyCode.W);
        bool leftPressed        = Input.GetKey(KeyCode.A);
        bool rightPressed       = Input.GetKey(KeyCode.D);
        bool runPressed         = Input.GetKey(KeyCode.LeftShift);


        // If runPressed is true then set to maximumRunVelocity, if false set to maximumWalkVelocity
        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;


        // Call Velocity functions
        ChangeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        LockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);


        // Update Animator
        animator.SetFloat(velocityXHash, velocityX);
        animator.SetFloat(velocityZHash, velocityZ);
    }



    /**
     *  CLASS FUNCTIONS
     * */

    // Handle acceleration and decceleration
    private void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {

        // Handle movement in the z direction
        if (forwardPressed && velocityZ < currentMaxVelocity)
            velocityZ += Time.deltaTime * acceleration;


        // Handle movement in the x direction
        if (leftPressed && velocityX > -currentMaxVelocity)
            velocityX -= Time.deltaTime * acceleration;

        if (rightPressed && velocityX < currentMaxVelocity)
            velocityX += Time.deltaTime * acceleration;


        // Handle decceleration
        if (!forwardPressed && velocityZ > 0.0f)
            velocityZ -= Time.deltaTime * decceleration;

        if (!leftPressed && velocityX < 0.0f)
            velocityX += Time.deltaTime * decceleration;

        if (!rightPressed && velocityX > 0.0f)
            velocityX -= Time.deltaTime * decceleration;


    }


    private void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        // Lock forward
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
            velocityZ = currentMaxVelocity;
        // Deccelerate to max walk speed
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * decceleration;
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.5f))
                velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
            velocityZ = currentMaxVelocity;


        // Lock Left
        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
            velocityX = -currentMaxVelocity;
        // Deccelerate to max walk speed
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * decceleration;
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity + 0.5f))
                velocityX = -currentMaxVelocity;

        }
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity - 0.05f))
            velocityX = -currentMaxVelocity;


        // Lock Right
        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
            velocityX = currentMaxVelocity;
        // Deccelerate to max walk speed
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * decceleration;
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.5f))
                velocityX = currentMaxVelocity;
        }
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
            velocityX = currentMaxVelocity;






        // Reset Velocity
        if (!forwardPressed && velocityZ < 0.0f)
            velocityZ = 0.0f;

        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
            velocityX = 0.0f;

    }
}
