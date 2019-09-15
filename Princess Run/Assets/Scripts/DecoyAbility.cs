using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DecoyAbility : Ability
{
    [Range(5, 10)]
    [SerializeField]
    protected int totalTime;

    private float timePassed;

    public GameObject decoyPreFab;
    private GameObject decoy;

    public override void StartAbility(Player player)
    {
        decoy = PhotonNetwork.Instantiate("Decoy", player.transform.position - player.transform.forward, player.transform.rotation);

        base.StartAbility(player);

        onGoingEvent = true;
        timePassed = 0;
    }

    public override void EndAbility(Player player)
    {
        base.EndAbility(player);

        PhotonNetwork.Destroy(decoy);

        onGoingEvent = false;
    }

    public override void Execute(Player player)
    {
        timePassed += Time.deltaTime;

        if (timePassed >= totalTime)
        {
            EndAbility(player);
        }
    }
}
