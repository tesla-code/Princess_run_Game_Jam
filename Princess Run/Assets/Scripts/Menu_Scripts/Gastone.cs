﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gastone : MonoBehaviour
{
    
    Vector3 orginialSize;

    // Path to gastone song
    public AudioSource src;
    

    // Start is called before the first frame update
    void Start()
    {
        orginialSize = gameObject.transform.localScale;
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseOver()
    {
        Debug.Log("Mouse Hover On Gaston");
        // 1. scale
        // transform.parent.gameObject
        gameObject.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);

        // 2. outline

        // 3. play song
        src.Play();
    }

    public void OnMouseExit()
    {
        // 1. scale back to normal orginialSize
        // gameObject.transform.localScale -= new Vector3(-0.2f, -0.2f, -0.2f);
        gameObject.transform.localScale = orginialSize;

        // 2. remove outline

        // 3. stop song
        src.Stop();
    }

    public void OnMouseDown()
    {
        // 1. Detect mouse click
        Debug.Log("Mouse is clicked on Gastone");

        // 2. Set hunter in pref script to true
        Pref.hunter = true;
        Debug.Log("Hunter bool is now = " + Pref.hunter);

    }
}
