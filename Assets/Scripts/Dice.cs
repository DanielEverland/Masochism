using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    /// <summary>
    /// The side that currently faces the camera the most
    /// </summary>
    public Side BestSide { get; private set; }

    /// <summary>
    /// Raised when the dice lands somewhere and a value has been chosen as the selected value.
    /// </summary>
    public UnityEvent<int> OnSelectedValue;

    private Vector3 DirectionToCamera => -Camera.main.transform.forward;

    private List<Side> _sides;
    private bool _hasBeenMoving;
    
    [SerializeField]
    private PlayerController _playerController;

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
        if (_hasBeenMoving && !_playerController.IsMoving)
        {
            _hasBeenMoving = false;

            SelectValue();
        }
        else if (!_hasBeenMoving && _playerController.IsMoving)
        {
            _hasBeenMoving = true;
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
        BestSide.ToggleHighlight(_playerController.IsMoving ? SideState.Facing : SideState.Active);
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
