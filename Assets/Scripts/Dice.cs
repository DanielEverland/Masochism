using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines information about a specific side of a dice
/// </summary>
[System.Serializable]
public struct Side
{
    public int Number;
    public Vector3 LocalDirection;
}

public class Dice : MonoBehaviour
{
    /// <summary>
    /// The side that currently faces the camera the most
    /// </summary>
    public Side BestSide { get; private set; }

    [SerializeField]
    private List<Side> _sides;

    private Vector3 DirectionToCamera => -Camera.main.transform.forward;

    private void Update()
    {
        QueryBestSide();
    }

    private void QueryBestSide()
    {
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
    }

    private float GetDotProductOfSide(Side side)
    {
        Vector3 worldDirection = transform.TransformDirection(side.LocalDirection.normalized);
        return Vector3.Dot(worldDirection, DirectionToCamera);
    }

    private void OnDrawGizmosSelected()
    {
        foreach (Side side in _sides)
        {
            Vector3 worldDirection = transform.TransformDirection(side.LocalDirection.normalized);
            Gizmos.DrawLine(transform.position, transform.position + worldDirection * 2.0f);
        }
    }
}
