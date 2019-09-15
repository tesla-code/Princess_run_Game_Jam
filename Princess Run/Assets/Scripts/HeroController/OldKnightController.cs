using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Photon.Pun;

public class OldKnightController : MonoBehaviourPunCallbacks
{
    float speed = 10;
    float rotSpeed = 80;
    float rot = 0f; // our rotation
    float gravity = 8;

    // Jumping Code
    bool jump;
    Rigidbody rb;
    public float jumpForce;

    Vector3 moveDir = Vector3.zero; // same as Vector3(0,0,0)

    // Refrences the controllers and animators that are on our player
    CharacterController controller;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    // Fixed update hmmm []
    void Update()
    {
        // TO-DO
        // Networking code here
        if (!photonView.IsMine)
        {
            return;
        }

        Movement();
        GetInput();
    }

    void Movement()
    {

        // check if we are on ground, 
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if(anim.GetBool("attacking") ==  true)
                {
                    return;
                }
                else if(anim.GetBool("attacking") == false)
                {
                    anim.SetBool("running", true);
                    anim.SetInteger("condition", 1);
                    moveDir = new Vector3(0, 0, 1);
                    moveDir *= speed;
                    moveDir = transform.TransformDirection(moveDir); // from local space to world space
                }
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                moveDir = new Vector3(0, 0, 0); // we don't move any more
            }

            // Trying to add jump
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Knight is trying to jump");
                rb.AddForce(Vector3.up * jumpForce);
                // isJumping = false;
            }

            rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, rot, 0);

            // Gravity moves us down
            moveDir.y -= gravity * Time.deltaTime;
            controller.Move(moveDir * Time.deltaTime);
            
        }
    }

    // Input related to abilites etc, direct movement is in void Movement() function
    void GetInput()
    {
        // Maybe remove this, since we may want the knight to be able to 
        // attack if he is in the air or something.
        if(controller.isGrounded)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (anim.GetBool("running") == true)
                {
                    anim.SetBool("running", false);
                    anim.SetInteger("condition", 0);
                }

                if(anim.GetBool("running") == false)
                {
                    Attacking();
                }
            }
        }
    }

    void Attacking()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        anim.SetBool("attacking", true);
        anim.SetInteger("condition", 2);

        yield return new WaitForSeconds(1);

        anim.SetInteger("condition", 0);
        anim.SetBool("attacking", false);
    }
    
}
