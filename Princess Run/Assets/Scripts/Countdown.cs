using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Countdown : MonoBehaviourPunCallbacks
{
    public float totalTime;
    public float currentTime;
    public bool stop = false;
    Text timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Countdown Timer").GetComponent<Text>();
        currentTime = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            currentTime -= Time.deltaTime;
            //update countdown timer
            timer.text = Mathf.RoundToInt(currentTime).ToString();

            if (currentTime <= 0f)
            {
                //game over, princess wins
                timer.text = "PRINCESS WINS!";
                photonView.RPC("SomeoneWon", RpcTarget.All, false);
            }
        }
    }
}
