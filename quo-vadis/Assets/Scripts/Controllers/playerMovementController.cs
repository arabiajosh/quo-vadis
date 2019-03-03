using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementController : MonoBehaviour
{

    private float maxSpeed;
    private float acceleration;

    public PlayerManager playerManager;
    private Rigidbody2D rb;
    private Animator anim;

    private float xSpeed = 0;
    private float ySpeed = 0;

    private static readonly float EPSILON = .005f;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = playerManager.maxSpeed;
        acceleration = playerManager.acceleration;
        rb = playerManager.rb;
        anim = playerManager.anim;
        anim.SetFloat("yLastDir", -1); //The character faces down orginally
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        rb.velocity = new Vector2(xSpeed, ySpeed);
        SetAnimationParameters();
    }


    /**
     * Implements 8-directional player movement using the WASD keyscheme. Movement
     * parameters are specified in the PlayerManager.cs script.
     */
    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            xSpeed = Mathf.Clamp(xSpeed + acceleration, -maxSpeed, maxSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            xSpeed = Mathf.Clamp(xSpeed - acceleration, -maxSpeed, maxSpeed);
        }

        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            if(xSpeed >= 0f)
            {
                xSpeed = Mathf.Clamp(xSpeed - acceleration, 0f, maxSpeed);
            }
            else
            {
                xSpeed = Mathf.Clamp(xSpeed + acceleration, -maxSpeed, 0f);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            ySpeed = Mathf.Clamp(ySpeed + acceleration, -maxSpeed, maxSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            ySpeed = Mathf.Clamp(ySpeed - acceleration, -maxSpeed, maxSpeed);
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if (ySpeed >= 0f)
            {
                ySpeed = Mathf.Clamp(ySpeed - acceleration, 0f, maxSpeed);
            }
            else
            {
                ySpeed = Mathf.Clamp(ySpeed + acceleration, -maxSpeed, 0f);
            }
        }
    }

    /**
    * Sets parameters in the animator associated with this game object such that
    * the animation state matches the direction of movement of the player.  
    */
    private void SetAnimationParameters()
    {
        anim.SetFloat("xSpeed", xSpeed);
        anim.SetFloat("ySpeed", ySpeed);
        if(System.Math.Abs(xSpeed) < EPSILON)
        {
            anim.SetBool("isIdleX", true);
        }
        else
        {
            anim.SetBool("isIdleX", false);
        }
        if (System.Math.Abs(ySpeed) < EPSILON)
        {
            anim.SetBool("isIdleY", true);
        }
        else
        {
            anim.SetBool("isIdleY", false);
        }

        //Set Last Dir if applicable
        if(xSpeed != 0 || ySpeed != 0)
        {
            anim.SetFloat("xLastDir", xSpeed);
            anim.SetFloat("yLastDir", ySpeed);
        }

    }
}
