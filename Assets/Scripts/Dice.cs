using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum DiceState
{
    Idle,
    Moving,
    Blocked,
}

public class Dice : MonoBehaviour
{
    /// <summary>
    /// The side that currently faces the camera the most
    /// </summary>
    public Side BestSide { get; private set; }

    /// <summary>
    /// Determines what state the dice is in
    /// </summary>
    public DiceState State { get; private set; }

    /// <summary>
    /// Raised when the dice lands somewhere and a value has been chosen as the selected value.
    /// </summary>
    public UnityEvent<int> OnSelectedValue;

    private Vector3 DirectionToCamera => -Camera.main.transform.forward;

    private List<Side> _sides;
    
    [SerializeField]
    private PlayerController _playerController;

    public void Block()
    {
        State = DiceState.Blocked;
    }

    public void Unblock()
    {
        State = _playerController.IsMoving ? DiceState.Moving : DiceState.Idle;
    }

    private void Awake()
    {
        _sides = new List<Side>(GetComponentsInChildren<Side>());
    }

    private void Update()
    {
        QueryBestSide();
        QueryHasLanded();
    }

    private void QueryHasLanded()
    {
        // We don't want to invoke new values while the dice is blocked
        if (State == DiceState.Blocked)
            return;

        if (State == DiceState.Moving && !_playerController.IsMoving)
        {
            State = DiceState.Idle;

            SelectValue();
        }
        else if (State == DiceState.Idle && _playerController.IsMoving)
        {
            State = DiceState.Moving;
        }
    }

    private void SelectValue()
    {
        OnSelectedValue.Invoke(BestSide.Number);
    }

    private void QueryBestSide()
    {
        Side oldBestSide = BestSide;

        float bestDotProduct = GetDotProductOfSide(_sides[0]);
        BestSide = _sides[0];

        for (int i = 1; i < _sides.Count; i++)
        {
            float currentDotProduct = GetDotProductOfSide(_sides[i]);

            // If a dot product is larger than another, it will point more in the direction of the camera
            if (currentDotProduct > bestDotProduct)
            {
                BestSide = _sides[i];
                bestDotProduct = currentDotProduct;
            }
        }

        oldBestSide?.ToggleHighlight(SideState.NotFacing);
        BestSide.ToggleHighlight(GetSideState());
    }

    private SideState GetSideState()
    {
        if (State == DiceState.Blocked)
            return SideState.Disabled;

        return _playerController.IsMoving ? SideState.Facing : SideState.Active;
    }

    private float GetDotProductOfSide(Side side)
    {
        Vector3 worldDirection = side.WorldDirection.normalized;
        return Vector3.Dot(worldDirection, DirectionToCamera);
    }

    private void OnValidate()
    {
        _playerController = GetComponent<PlayerController>();
    }
}
