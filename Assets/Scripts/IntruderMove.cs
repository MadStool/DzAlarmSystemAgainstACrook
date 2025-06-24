using UnityEngine;

public interface IIntruder { }

[RequireComponent(typeof(Rigidbody))]
public class IntruderMove : MonoBehaviour, IIntruder
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    [SerializeField, Range(1f, 10f)]

    private float _moveSpeed = 5f;
    private Collider _collider;

    private void Awake()
    {
        if (TryGetComponent(out _collider) == false)
        {
            _collider = gameObject.AddComponent<CapsuleCollider>();
            Debug.LogWarning($"Added CapsuleCollider to {name}", this);
        }
    }

    private void Update()
    {
        var input = new Vector3(
            Input.GetAxis(HorizontalAxis),
            0,
            Input.GetAxis(VerticalAxis)
        );

        transform.Translate(input * (_moveSpeed * Time.deltaTime));
    }
}