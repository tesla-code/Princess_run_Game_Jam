using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : AbstractState
{
    public override void Enter()
    {
        base.Enter();

        _animator.SetBool("IsWalking", true);
    }

    public override void Execute(PlayerController player)
    {
        
    }

    public override void Exit()
    {
        base.Exit();

        _animator.SetBool("IsWalking", false);
    }
}
