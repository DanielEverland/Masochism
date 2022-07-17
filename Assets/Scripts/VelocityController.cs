using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VelocityController : MonoBehaviour
{
    private static readonly float MIN_VELOCITY = 0.05f;
    private static readonly float MIN_ANGULARVELOCITY = 0.03f;

    [SerializeField]
    private Rigidbody _rigidBody;

    private int _collisionCounter = 0;
    private bool IsVelocityLow => _rigidBody.velocity.magnitude <= MIN_VELOCITY;
    private bool IsAngularVelocityLow => _rigidBody.angularVelocity.magnitude <= MIN_ANGULARVELOCITY;

    // Update is called once per frame
    void Update()
    {
        if (_rigidBody.velocity.magnitude > Mathf.Abs(MIN_VELOCITY) || _rigidBody.angularVelocity.magnitude > Mathf.Abs(MIN_ANGULARVELOCITY))
        {
            Debug.Log("Velocity: " + _rigidBody.velocity.magnitude + " Angular Velocity: " + _rigidBody.angularVelocity.magnitude);
        }
        DisableVelocity();
    }

    private void DisableVelocity()
    {
        if (IsVelocityLow && IsAngularVelocityLow && _collisionCounter > 0)
        {
            _rigidBody.isKinematic = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collisionCounter++;
    }

    private void OnCollisionExit(Collision collision)
    {
        _collisionCounter--;
    }
}
