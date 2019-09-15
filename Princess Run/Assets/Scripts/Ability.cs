using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public bool onGoingEvent;

    public virtual void StartAbility(Player player)
    {

    }

    public virtual void EndAbility(Player player)
    {

    }

    public abstract void Execute(Player player);
}
