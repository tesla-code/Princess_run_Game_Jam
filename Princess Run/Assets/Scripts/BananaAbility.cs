using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BananaAbility : Ability
{

    //WARNING
    /*
     You have to create an empty game object outside the Camera component in order to be able to spawn the "bananas"
         */

    public GameObject bananaPrefab;

    public override void StartAbility(Player player)
    {
        base.StartAbility(player);

        //throw banana
        GameObject banana = PhotonNetwork.Instantiate("Banana", transform.position + transform.forward * 2 + transform.up * 2, transform.rotation);
        // GameObject banana = Instantiate(bananaPrefab, transform.position+transform.forward*2+transform.up*2, transform.rotation);
        banana.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up) * 450f);

        onGoingEvent = true;
    }

    public override void EndAbility(Player player)
    {
        base.EndAbility(player);

        onGoingEvent = false;
    }

    public override void Execute(Player player)
    {
        EndAbility(player);
    }
}
