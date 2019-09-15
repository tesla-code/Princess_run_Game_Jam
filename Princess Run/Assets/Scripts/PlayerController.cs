using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private AbstractState _currentState;

    [SerializeField]
    private List<AbstractState> _availableStates;

    public AbstractState CurrentState { get { return _currentState; } }

    private bool idle, walking, running, backwards;

    private void Awake()
    {
        CacheAllStates();
        _currentState = GetComponentInChildren<IdleState>();
        _currentState.Enter();

        idle = true;
        
        
        walking = running = backwards = false;
    }

    private void CacheAllStates()
    {
        _availableStates = GetComponentsInChildren<AbstractState>(true).ToList();
    }

    public TState FindAvailableState<TState>()
        where TState : AbstractState
    {
        foreach (var availableState in _availableStates)
        {
            if (availableState.GetType() == typeof(TState))
            {
                return availableState as TState;
            }
        }

        return null;
    }

    public void ChangeState(AbstractState newState)
    {
        if (_currentState == newState)
            return;
        
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    private void Update()
    {
        bool changedState = false;

        if (_currentState != null)
        {
            _currentState.Execute(this);
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
        {
            idle = true;
            walking = false;
            running = false;
            backwards = false;

            changedState = true;
        }

        //Check if input for walking is given
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            idle = false;

            CheckShift();

            changedState = true;
        }

        //Check if input for walking backwards is given
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            idle = false;
            backwards = true;

            CheckShift();

            changedState = true;
        }

        //Check if input for starting to spring is given
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (walking)
            {
                walking = false;
                running = true;
                changedState = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                idle = false;
                backwards = true;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    running = true;
                }
                else
                {
                    walking = true;
                }
            }

            else
            {
                idle = true;
                walking = false;
                running = false;
                backwards = false;
            }

            changedState = true;
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            backwards = false;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                idle = false;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    running = true;
                }
                else
                {
                    walking = true;
                }
            }

            else
            {
                idle = true;
                walking = false;
                running = false;
                backwards = false;
            }

            changedState = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (running)
            {
                walking = true;
                running = false;
                changedState = true;
            }
        }

        if (changedState)
        {
            FindState();
        }

    }

    //Check if the shift key is pressed
    private void CheckShift()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            running = true;
            GameObject.Find("Dragon(Clone)").GetComponent<DragonAnim>().UpdateAnim(1);
        }

        else
        {
            walking = true;
            GameObject.Find("Dragon(Clone)").GetComponent<DragonAnim>().UpdateAnim(2);
        }
    }

    private void FindState()
    {
        if (idle == true)
        {
            var IdleS = FindAvailableState<IdleState>();
            ChangeState(IdleS);
        }

        else if (walking == true && backwards == true)
        {
            var walkingS = FindAvailableState<BackwardsWalkingState>();
            ChangeState(walkingS);
        }

        else if (walking == true)
        {
            var walkingS = FindAvailableState<WalkingState>();
            ChangeState(walkingS);
        }

        else if (running == true && backwards == true)
        {
            var runningS = FindAvailableState<BackwardsRunningState>();
            ChangeState(runningS);
        }

        else if (running == true)
        {
            var runningS = FindAvailableState<RunningState>();
            ChangeState(runningS);
        }

        else
        {
            var IdleS = FindAvailableState<IdleState>();
            ChangeState(IdleS);
        }
    }
}
