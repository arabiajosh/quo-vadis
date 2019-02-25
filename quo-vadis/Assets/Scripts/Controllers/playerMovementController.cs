using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementController : MonoBehaviour
{

    private float maxSpeed;
    private float acceleration;

    public PlayerManager playerManager;
    private Rigidbody2D rb;

    private float xSpeed = 0;
    private float ySpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = playerManager.maxSpeed;
        acceleration = playerManager.acceleration;
        rb = playerManager.rb;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        rb.velocity = new Vector2(xSpeed, ySpeed);
    }

    private void move()
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
}
