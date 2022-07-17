using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField]
    private float OffsetRange = 0.5f;

    [SerializeField]
    private bool _shakingEnabled;

    public void Toggle(bool shakingEnabled)
    {
        _shakingEnabled = shakingEnabled;

        if(!shakingEnabled)
            transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        if (!_shakingEnabled)
            return;

        transform.localPosition = new Vector3()
        {
            x = Random.Range(-OffsetRange, OffsetRange),
            y = Random.Range(-OffsetRange, OffsetRange),
            z = Random.Range(-OffsetRange, OffsetRange),
        };
    }
}
