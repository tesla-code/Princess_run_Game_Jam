using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightDashAbility : KnightAbility
{
    private float timePassed;

    [Range(1, 5)]
    [SerializeField]
    protected int totalTime;

    private Camera _camera;


    public override void StartAbility(KnightController knight)
    {
        base.StartAbility(knight);

        knight.speed *= 1.5f;
        timePassed = 0.0f;

        _camera = knight.GetComponentInChildren<Camera>();
        _camera.fieldOfView *= 1.1f;

        Debug.Log("Event started");

        onGoingEvent = true;
    }

    public override void EndAbility(KnightController knight)
    {
        base.EndAbility(knight);

        knight.speed /= 1.5f;
        _camera.fieldOfView /= 1.1f;

        Debug.Log("Event ended");

        onGoingEvent = false;
    }

    public override void Execute(KnightController knight)
    {
        timePassed += Time.deltaTime;

        if (timePassed >= totalTime)
        {
            EndAbility(knight);
        }
    }
}
