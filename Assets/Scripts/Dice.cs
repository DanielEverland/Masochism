using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    /// <summary>
    /// The side that currently faces the camera the most
    /// </summary>
    public Side BestSide { get; private set; }
    
    private List<Side> _sides;
    private Vector3 DirectionToCamera => -Camera.main.transform.forward;

    private void Awake()
    {
        _sides = new List<Side>(GetComponentsInChildren<Side>());
    }

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
        Vector3 worldDirection = side.WorldDirection.normalized;
        return Vector3.Dot(worldDirection, DirectionToCamera);
    }
}
