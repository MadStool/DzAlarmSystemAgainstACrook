using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class IntruderMovement : Intruder
{
    public float moveSpeed = 5f;

    private void Start()
    {
        if (GetComponent<Collider>() == null)
        {
            gameObject.AddComponent<CapsuleCollider>();
        }
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, moveZ);
    }
}