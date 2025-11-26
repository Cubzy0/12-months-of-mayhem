using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private GameInput input;
    private Rigidbody2D rb;
    private Vector2 move;
    private Vector3 initialScale;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        input = new GameInput();
        rb = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale;
    }

    private void OnEnable() => input.Player.Enable();
    private void OnDisable() => input.Player.Disable();

    private void Update()
    {
        move = input.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = move * moveSpeed;
        HandleFlip();
    }

    private void HandleFlip()
    {
        if (move.x > 0.1f)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(initialScale.x),
                initialScale.y,
                initialScale.z
            );
        }
        else if (move.x < -0.1f)
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(initialScale.x),
                initialScale.y,
                initialScale.z
            );
        }
    }
}