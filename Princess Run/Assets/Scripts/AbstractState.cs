using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractState : MonoBehaviour
{
    [SerializeField]
    protected Animator _animator;

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public abstract void Execute(PlayerController player);
}
