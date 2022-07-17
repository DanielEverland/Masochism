using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    [SerializeField] private float StartDelay = 2.0f;
    [SerializeField] private Dice Target;
    [SerializeField] private float PanTime = 4.0f;
    [SerializeField] private MonoBehaviour NormalCameraComponent;

    private Vector3 StartPosition;

    private void Awake()
    {
        NormalCameraComponent.enabled = false;
        StartPosition = transform.position;
        Target.Block();
    }

    private void Update()
    {
        if (Time.time < StartDelay)
            return;

        if (Time.time > (StartDelay + PanTime))
        {
            NormalCameraComponent.enabled = true;
            Target.Unblock();
            Destroy(this);
        }
        else
        {
            float percentage = (Time.time - StartDelay) / PanTime;
            Vector3 currPosition = Vector3.Lerp(StartPosition, Target.transform.position, percentage);
            currPosition.z = StartPosition.z;
            transform.position = currPosition;
        }
    }
}
