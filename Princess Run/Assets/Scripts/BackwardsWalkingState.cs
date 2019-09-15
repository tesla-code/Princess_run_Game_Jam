using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardsWalkingState : AbstractState
{
    public override void Enter()
    {
        base.Enter();

        _animator.SetBool("IsWalking", true);
        _animator.SetBool("IsBackwards", true);
    }

    public override void Execute(PlayerController player)
    {

    }

    public override void Exit()
    {
        base.Exit();

        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsBackwards", false);
    }
}
