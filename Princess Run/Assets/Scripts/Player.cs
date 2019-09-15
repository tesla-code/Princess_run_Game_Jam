using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks
{
    #region Variables
    public float speed;
    public float jumpForce;
    public LayerMask ground;
    public GameObject cameraParent;
    public Transform groundDetector;
    private GameObject dragon;
    private Rigidbody dragonRb;
    public bool isFlying;
    private bool isStunned;

    private float stunTimer = 3f;
    private float stunTime = 3f;

    public Camera normalCam;

    float tempHmove;
    float tempVmove;
    bool sprint;
    bool jump;
    bool isJumping;

    private Transform camFlyPos;
    private Transform camStartPos;
    private Transform camPos;

    public float slopeForce;
    public float slopeForceRayLength;

    private float flyTime = 0.5f;
    private float flyTimer = 0.5f;

    private Rigidbody rb;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        camStartPos = GameObject.Find("StartCamPos").transform;
        camFlyPos = GameObject.Find("DragonCamPos").transform;
        camPos = camStartPos;

        dragon = GameObject.Find("Dragon(Clone)");
        dragonRb = dragon.GetComponent<Rigidbody>();
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
        if (isFlying) camPos = camFlyPos;
        else camPos = camStartPos;

        normalCam.transform.localPosition = camPos.localPosition;
        normalCam.transform.localRotation = camPos.localRotation;
        
        tempHmove = Input.GetAxisRaw("Horizontal");
        tempVmove = Input.GetAxisRaw("Vertical");
       
        //Controls
        sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        jump = Input.GetKey(KeyCode.Space);
        
        
    }
    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        
        
        if((tempHmove != 0 || tempVmove != 0) && OnSlope())
        {
            //rb.AddForce(Vector3.down * slopeForce * Time.deltaTime);
        }
        rb.AddForce(Vector3.down * slopeForce * Time.deltaTime);
        //States
        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, ground);
        isJumping = jump && isGrounded;
        bool isSprinting = sprint && tempVmove > 0 && !isJumping;

        
        //Jump
        if (isJumping && !isFlying)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isJumping = false;
        }

        flyTimer += Time.deltaTime;
        stunTimer += Time.deltaTime;
        if(isStunned && stunTimer >= stunTime)
        {
            isStunned = false;
            stunTimer = 0f;
        }
        if(isFlying && Input.GetKeyDown(KeyCode.Space) && flyTimer >= flyTime)
        {
            
            rb.AddForce(Vector3.up * 2000f, ForceMode.Impulse);
            flyTimer = 0f;
            
            GameObject.Find("Dragon(Clone)").GetComponent<DragonAnim>().UpdateAnim(3);
            StartCoroutine(ExecuteAfterTime(1));


        }
        if (isStunned) return;
        //Movement
        Vector3 tempDirection = new Vector3(tempHmove, 0, tempVmove);
        tempDirection.Normalize();

        float adjustedSpeed = speed;
        if (isSprinting) adjustedSpeed *= 1.5f;
        if (isFlying) adjustedSpeed *= 3f;
      
       
        Vector3 tempTargetVelocity = transform.TransformDirection(tempDirection) * adjustedSpeed * Time.deltaTime;
        tempTargetVelocity.y = rb.velocity.y;
        rb.velocity = tempTargetVelocity;
        
        if(isFlying)
        {
            rb.drag = 5f;
            dragon.transform.position = transform.position;
            dragon.transform.rotation = transform.rotation;
            
        }
        if (!isFlying) rb.drag = 0f;
        float maxSpeed = 40f;
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        
        yield return new WaitForSeconds(time);
        
        GameObject.Find("Dragon(Clone)").GetComponent<DragonAnim>().UpdateAnim(4);
        Debug.Log("Activsated");

    }

    [PunRPC]
    public void HitByBanana()
    {
        //display message: tripped on a banana
        Debug.Log("You got rekt by dem bananers");
        isStunned = true;
        stunTimer = 0f;
    }
    private void MoveDragon()
    {

        //Debug.Log("moving dragon");
        //Movement
        Vector3 tempDirection = new Vector3(tempHmove, 0, tempVmove);
        tempDirection.Normalize();

        float adjustedSpeed = 300f;
 
        Vector3 tempTargetVelocity = transform.TransformDirection(tempDirection) * adjustedSpeed * Time.deltaTime;
        tempTargetVelocity.y = dragonRb.velocity.y;

        dragonRb.velocity = tempTargetVelocity;
        gameObject.transform.localPosition = dragon.transform.localPosition;

        Debug.Log(dragonRb.velocity);

       
    }

    private bool OnSlope()
    {
        if(isJumping)
        {
            return false;
        }
        RaycastHit hit;
        if(Physics.Raycast(groundDetector.position, Vector3.down, out hit, slopeForceRayLength))
        {
            if(hit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
    }

    #endregion

   

    
}