using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Inputs")]
    [SerializeField] private float moveInputX;
    [SerializeField] private float moveInputY;

    private void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInputX * speed, rb.linearVelocity.y);
    }
}