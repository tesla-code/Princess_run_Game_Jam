using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Photon.Pun;

public class KnightController : MonoBehaviourPunCallbacks
{
    #region Variables
    public float speed;
    public float jumpForce;
    public LayerMask ground;
    public GameObject cameraParent;
    public Transform groundDetector;
    private bool isStunned;

    private float stunTimer = 3f;
    private float stunTime = 3f;

    public Camera normalCam;
    
    Animator anim;

    float tempHmove;
    float tempVmove;
    bool sprint;
    bool jump;
    bool isJumping;
    
    private Transform camStartPos;
    private Transform camPos;

    public float slopeForce;
    public float slopeForceRayLength;

    private Rigidbody rb;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        
        camStartPos = GameObject.Find("StartCamPos").transform;
        camPos = camStartPos;
        
        anim = GetComponent<Animator>();

        Debug.Log("Trying to enable hero camera");
        cameraParent.SetActive(photonView.IsMine);
        

        if (!photonView.IsMine)
        {
            gameObject.layer = 12;
        }

        if (Camera.main)
            Camera.main.enabled = false;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (!photonView.IsMine) return;

        camPos = camStartPos;

        normalCam.transform.localPosition = camPos.localPosition;
        normalCam.transform.localRotation = camPos.localRotation;

        tempHmove = Input.GetAxisRaw("Horizontal");
        tempVmove = Input.GetAxisRaw("Vertical");

        //Controls
        sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        jump = Input.GetKey(KeyCode.Space);

        GetInput();

        //Debug.Log($"The camera is: {cameraParent.activeSelf}");
    }
    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;


        if ((tempHmove != 0 || tempVmove != 0) && OnSlope())
        {
            //rb.AddForce(Vector3.down * slopeForce * Time.deltaTime);
        }
        rb.AddForce(Vector3.down * slopeForce * Time.deltaTime);
        //States
        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
        isJumping = jump && isGrounded;
        bool isSprinting = sprint && tempVmove > 0 && !isJumping;


        //Jump
        if (isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isJumping = false;
        }
        
        stunTimer += Time.deltaTime;
        if (isStunned && stunTimer >= stunTime)
        {
            isStunned = false;
            stunTimer = 0f;
        }

        if (isStunned) return;
        //Movement
        Vector3 tempDirection = new Vector3(tempHmove, 0, tempVmove);
        tempDirection.Normalize();

        float adjustedSpeed = speed;
        if (isSprinting) adjustedSpeed *= 1.5f;

        if (tempHmove > 0 || tempVmove > 0)
        {
            if (!anim.GetBool("attacking"))
            {
                anim.SetBool("running", true);
                anim.SetInteger("condition", 1);
            }
        }
        else
        {
            anim.SetBool("running", false);
            anim.SetInteger("condition", 0);
        }


        Vector3 tempTargetVelocity = transform.TransformDirection(tempDirection) * adjustedSpeed * Time.deltaTime;
        tempTargetVelocity.y = rb.velocity.y;
        rb.velocity = tempTargetVelocity;

        float maxSpeed = 40f;
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Princess")
        {
            // Call Win() function Vivi_Prefab
            Debug.Log("Hero won");
            photonView.RPC("SomeoneWon", RpcTarget.All, false);
        }
    }
    [PunRPC]
    public void SomeoneWon(bool princess)
    {
        GameObject.Find("Countdown Timer").GetComponent<Countdown>().stop = true;

        if (princess)
            GameObject.Find("Countdown Timer").GetComponent<Text>().text = "Princess won!";
        else
            GameObject.Find("Countdown Timer").GetComponent<Text>().text = "Hero won!";

        StartCoroutine(ExecuteAfterTime(15));
    }
    IEnumerator ExecuteAfterTime(float time)
    {

        yield return new WaitForSeconds(time);

        //reload scene
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().name);//prolly wont work

    }

    [PunRPC]
    public void HitByBanana()
    {
        //display message: tripped on a banana
        Debug.Log("You got rekt by dem bananers");
        isStunned = true;
        stunTimer = 0f;
    }

    private bool OnSlope()
    {
        if (isJumping)
        {
            return false;
        }
        RaycastHit hit;
        if (Physics.Raycast(groundDetector.position, Vector3.down, out hit, slopeForceRayLength))
        {
            if (hit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
    }

    // Input related to abilites etc, direct movement is in void Movement() function
    void GetInput()
    {
        // Maybe remove this, since we may want the knight to be able to 
        // attack if he is in the air or something.
        if (!isJumping)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (anim.GetBool("running") == true)
                {
                    anim.SetBool("running", false);
                    anim.SetInteger("condition", 0);
                }

                if (anim.GetBool("running") == false)
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

    #endregion
}
