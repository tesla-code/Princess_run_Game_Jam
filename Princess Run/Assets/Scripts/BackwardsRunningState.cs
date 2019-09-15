using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardsRunningState : AbstractState
{
    public override void Enter()
    {
        base.Enter();

        _animator.SetBool("IsRunning", true);
        _animator.SetBool("IsBackwards", true);
    }

    public override void Execute(PlayerController player)
    {

    }

    public override void Exit()
    {
        base.Exit();

        _animator.SetBool("IsRunning", false);
        _animator.SetBool("IsBackwards", false);
    }
}
