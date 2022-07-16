using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SideState
{
    Active,
    Facing,
    NotFacing,
    Disabled,
}

public class Side : MonoBehaviour
{
    public Vector3 WorldDirection => -transform.forward;

    [SerializeField]
    public int Number;

    /// <summary>
    /// Color to show when the player is ready to jump
    /// </summary>
    [SerializeField]
    private Color _activeColor = Color.green;

    /// <summary>
    /// Color to show when this side is facing the camera
    /// </summary>
    [SerializeField]
    private Color _highlightedColor = Color.white;

    /// <summary>
    /// Color to show when this side foes not face the camera
    /// </summary>
    [SerializeField]
    private Color _notHighlightedColor = Color.white;

    /// <summary>
    /// Color to show when the dice is blocked
    /// </summary>
    [SerializeField]
    private Color _blockedColor = Color.red;
    
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    
    public void ToggleHighlight(SideState state)
    {
        _spriteRenderer.color = StateToColor(state);
    }

    private Color StateToColor(SideState state)
    {
        return state switch
        {
            SideState.Active => _activeColor,
            SideState.Facing => _highlightedColor,
            SideState.Disabled => _blockedColor,
            _ => _notHighlightedColor
        };
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + WorldDirection);
    }

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
