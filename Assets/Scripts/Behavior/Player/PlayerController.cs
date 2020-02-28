using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	Animator animator;
    Rigidbody2D rigidBody;
	public float playerVelocity = 3f;
    public float playerJumpForce = 2f;

	void Start()
	{
		animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        groundSensor = transform.Find("GroundSensor").GetComponent<PlayerGroundSensor>();
    }

    private PlayerGroundSensor groundSensor;
    private bool isPlayerGrounded = false;
    private bool isCombatIdle = false;
    private bool isPlayerDead = false;

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        //Check if character just landed on the ground
        if (!isPlayerGrounded && groundSensor.State())
        {
            isPlayerGrounded = true;
            animator.SetBool("Grounded", isPlayerGrounded);
        }

        //Check if character just started falling
        if (isPlayerGrounded && !groundSensor.State())
        {
            isPlayerGrounded = false;
            animator.SetBool("Grounded", isPlayerGrounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if (inputX < 0)
            GetComponent<SpriteRenderer>().flipX = false;

        // Move
        rigidBody.velocity = new Vector2(inputX * playerVelocity, rigidBody.velocity.y);

        //Set AirSpeed in animator
        animator.SetFloat("AirSpeed", rigidBody.velocity.y);

        // -- Handle Animations --
        //Death
        if (Input.GetKeyDown("e"))
        {
            if (!isPlayerDead)
                animator.SetTrigger("Death");
            else
                animator.SetTrigger("Recover");

            isPlayerDead = !isPlayerDead;
        }

        //Hurt
        else if (Input.GetKeyDown("q"))
            animator.SetTrigger("Hurt");

        //Attack
        else if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }

        //Change between idle and combat idle
        else if (Input.GetKeyDown("f"))
            isCombatIdle = !isCombatIdle;

        //Jump
        else if (Input.GetKeyDown("space") && isPlayerGrounded)
        {
            animator.SetTrigger("Jump");
            isPlayerGrounded = false;
            animator.SetBool("Grounded", isPlayerGrounded);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, playerJumpForce);
            groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (isCombatIdle)
            animator.SetInteger("AnimState", 1);

        //Idle
        else
            animator.SetInteger("AnimState", 0);
    }

}
