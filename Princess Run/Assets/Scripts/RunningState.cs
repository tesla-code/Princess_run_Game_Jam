using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : AbstractState
{
    public override void Enter()
    {
        base.Enter();

        _animator.SetBool("IsRunning", true);
    }

    public override void Execute(PlayerController player)
    {

    }

    public override void Exit()
    {
        base.Exit();

        _animator.SetBool("IsRunning", false);
    }
}
