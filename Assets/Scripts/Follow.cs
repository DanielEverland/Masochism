using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private float _followSpeed;

    private void Update()
    {
        Vector3 newPosition = Vector2.Lerp(transform.position, _target.transform.position, Time.deltaTime * _followSpeed);
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
