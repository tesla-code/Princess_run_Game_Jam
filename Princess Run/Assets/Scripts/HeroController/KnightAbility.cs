using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KnightAbility : MonoBehaviour
{
    public bool onGoingEvent;

    public virtual void StartAbility(KnightController knight)
    {

    }

    public virtual void EndAbility(KnightController knight)
    {

    }

    public abstract void Execute(KnightController knight);
}
