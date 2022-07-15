using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side : MonoBehaviour
{
    [SerializeField]
    public int Number;

    public Vector3 WorldDirection => -transform.forward;
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + WorldDirection);
    }
}
