using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// The mouse position in world space relative to the player.
    /// </summary>
    public Vector2 RelativeMousePosition { get; private set; }

    /// <summary>
    /// Determines if object is moving linearly og angularly
    /// </summary>
    public bool IsMoving => !IsNearlyZero(_rigidbody.angularVelocity.sqrMagnitude) || !IsNearlyZero(_rigidbody.velocity.sqrMagnitude);

    /// <summary>
    /// Raised when the player clicks the left mouse button.
    /// The vector is the relative position in world space.
    /// </summary>
    [SerializeField]
    private UnityEvent<Vector2> _onLeftMouseClicked;

    /// <summary>
    /// Should input querying be enabled while the object is moving?
    /// </summary>
    [SerializeField]
    private bool _permitInputWhileMoving;
    
    [SerializeField]
    private Rigidbody _rigidbody;

    void Update()
    {
        UpdateMousePositionInWorld();
        QueryMouseInput();
    }

    private void UpdateMousePositionInWorld()
    {
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RelativeMousePosition = mousePositionInWorld - transform.position;
    }

    private void QueryMouseInput()
    {
        if (IsMoving && !_permitInputWhileMoving)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _onLeftMouseClicked.Invoke(RelativeMousePosition);
        }
    }

    private bool IsNearlyZero(float value)
    {
        return Mathf.Abs(value) < float.Epsilon;
    }

    private void OnValidate()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
