using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class IntruderMove : MonoBehaviour, IIntruder
{
    [SerializeField, Range(1f, 10f)] private float _moveSpeed = 5f;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    private void Update()
    {
        Vector3 input = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical"));

        _rigidbody.MovePosition(transform.position + input * (_moveSpeed * Time.deltaTime));
    }
}