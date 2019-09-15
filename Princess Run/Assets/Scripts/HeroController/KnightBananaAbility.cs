﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class KnightBananaAbility : KnightAbility
{
    //WARNING
    /*
     You have to create an empty game object outside the Camera component in order to be able to spawn the "bananas"
         */

    public GameObject bananaPrefab;

    public override void StartAbility(KnightController knight)
    {
        base.StartAbility(knight);

        //throw banana
        GameObject banana = PhotonNetwork.Instantiate("Banana", transform.position + transform.forward * 2 + transform.up * 2, transform.rotation);
        // GameObject banana = Instantiate(bananaPrefab, transform.position+transform.forward*2+transform.up*2, transform.rotation);
        banana.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * 450f);
    }

    public override void EndAbility(KnightController knight)
    {
        base.EndAbility(knight);
    }

    public override void Execute(KnightController knight)
    {
        EndAbility(knight);
    }
}
