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
    
    public bool CanJump => !IsMoving || HasJumpsLeft();

    public int TimesCanJumpBetweenStops
    {
        get => _timesCanJumpBetweenStopping;
        set => _timesCanJumpBetweenStopping = value;
    }

    /// <summary>
    /// Raised when the player clicks the left mouse button.
    /// The vector is the relative position in world space.
    /// </summary>
    [SerializeField]
    private UnityEvent<Vector2> _onLeftMouseClicked;

    /// <summary>
    /// Amount of times the dice can jump between stopping
    /// </summary>
    [SerializeField]
    private int _timesCanJumpBetweenStopping = 1;
    
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private Jumper _jumper;
    
    private bool _shouldJump;
    
    private void Update()
    {
        UpdateMousePositionInWorld();
        QueryMouseInput();
    }

    private void FixedUpdate()
    {
        if(_shouldJump)
        {
            _shouldJump = false;
            _onLeftMouseClicked.Invoke(RelativeMousePosition);
        }
    }

    private void UpdateMousePositionInWorld()
    {
        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RelativeMousePosition = mousePositionInWorld - transform.position;
    }

    private void QueryMouseInput()
    {
        if (!CanJump)
            return;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _shouldJump = true;
        }
    }
    
    private bool HasJumpsLeft()
    {
        return _jumper.TimesJumped < _timesCanJumpBetweenStopping;
    }

    private bool IsNearlyZero(float value)
    {
        return Mathf.Abs(value) < float.Epsilon;
    }

    private void OnValidate()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _jumper = GetComponent<Jumper>();
    }
}
