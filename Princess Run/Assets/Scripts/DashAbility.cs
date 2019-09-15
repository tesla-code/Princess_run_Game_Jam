using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : Ability
{
    private float timePassed;

    [Range(1,5)]
    [SerializeField]
    protected int totalTime;

    private Camera _camera;


    public override void StartAbility(Player player)
    {
        base.StartAbility(player);

        player.speed *= 1.5f;
        timePassed = 0.0f;

        _camera = player.GetComponentInChildren<Camera>();
        _camera.fieldOfView *= 1.1f;

        Debug.Log("Event started");

        onGoingEvent = true;
    }

    public override void EndAbility(Player player)
    {
        base.EndAbility(player);

        player.speed /= 1.5f;
        _camera.fieldOfView /= 1.1f;

        Debug.Log("Event ended");

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
