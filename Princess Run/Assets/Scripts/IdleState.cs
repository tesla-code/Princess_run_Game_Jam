using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AbstractState
{
    public override void Enter()
    {
        base.Enter();

        _animator.SetBool("IsIdling", true);
        _animator.SetBool("Idle1", (Random.Range(0, 1) == 1) ? true : false);
    }

    public override void Execute(PlayerController player)
    {
        
    }

    public override void Exit()
    {
        base.Exit();

        _animator.SetBool("IsIdling", false);
    }
}
