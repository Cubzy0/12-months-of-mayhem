using System.Collections;
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

    // Speed boost support
    private float baseMoveSpeed;
    private bool isSpeedBoostActive;
    private Coroutine speedBoostRoutine;

    private void Awake()
    {
        input = new GameInput();
        rb = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale;

        baseMoveSpeed = moveSpeed; // remember the original speed
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

    // ---- SPEED BOOST API ----
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        if (speedBoostRoutine != null)
        {
            StopCoroutine(speedBoostRoutine);
        }

        speedBoostRoutine = StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
    {
        isSpeedBoostActive = true;
        moveSpeed = baseMoveSpeed * multiplier;

        yield return new WaitForSeconds(duration);

        moveSpeed = baseMoveSpeed;
        isSpeedBoostActive = false;
        speedBoostRoutine = null;
    }
}