using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private float timer = 300f;
    private float currentTime;
    private bool activated = false;
    AudioSource audio;
    AudioSource audio2;
    private float pitchTimer = 300;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.Find("Background Music").GetComponent<AudioSource>();
        audio2 = GameObject.Find("Background Music2").GetComponent<AudioSource>();
        
        currentTime = timer;   
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        //Debug.Log(timer);
        if(currentTime < 60f)
        {
            if (!activated)
            {
                activated = true;
                audio.Pause();
                audio2.Play(0);
            }
            pitchTimer += Time.deltaTime;
            audio2.pitch = pitchTimer / 300f;
            if(currentTime <= 0f)
            {
                audio2.Pause();
            }
        }
    }
}
