using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAbility : Ability
{
    public override void StartAbility(Player player)
    {
        base.StartAbility(player);

        onGoingEvent = true;
    }

    public override void EndAbility(Player player)
    {
        base.EndAbility(player);

        onGoingEvent = false;
    }

    public override void Execute(Player player)
    {

    }
}
