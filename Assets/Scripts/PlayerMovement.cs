using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 movement;
    private Rigidbody2D rb;

    [SerializeField] private float playerSpeed = 5;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            rb.velocity = movement * playerSpeed;
        }
    }
}
