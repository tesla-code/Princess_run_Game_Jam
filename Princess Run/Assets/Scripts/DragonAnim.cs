using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnim : MonoBehaviour
{
    Animator anim;
    public bool isWalking;
    public bool isRunning;
    public bool isGliding;
    public bool isFlying;
    public bool isIdle;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateAnim(int newState)
    {
        isWalking = false;
        isRunning = false;
        isGliding = false;
        isFlying = false;
        isIdle = false;
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isFlying", false);
        anim.SetBool("isGliding", false);
        if (newState == 0)
            isIdle = true;

        if (newState == 1)
            isWalking = true;

        if (newState == 2)
            isRunning = true;

        if (newState == 3)
            isFlying = true;

        if (newState == 4)
            isGliding = true;

        if (isWalking)
            anim.SetBool("isWalking", true);

        if (isIdle)
            anim.SetBool("isIdle", true);

        if (isRunning)
            anim.SetBool("isRunning", true);

        if (isFlying)
            anim.SetBool("isFlying", true);

        if (isGliding)
            anim.SetBool("isGliding", true);
    }
}
