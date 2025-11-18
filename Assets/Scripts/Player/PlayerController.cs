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

    [Header("Safe Area Clamp")]
    [SerializeField] private bool clampToSafeArea = true;
    [SerializeField] private float paddingX = 0.5f; // extra space so player doesn't touch the edge
    [SerializeField] private float paddingY = 0.5f;

    private Camera mainCam;
    private Vector2 worldMin;   // bottom-left of safe area in world space
    private Vector2 worldMax;   // top-right of safe area in world space

    private void Awake()
    {
        input = new GameInput();
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        initialScale = transform.localScale;
    }

    private void OnEnable() => input.Player.Enable();
    private void OnDisable() => input.Player.Disable();

    private void Start()
    {
        if (clampToSafeArea)
            CalculateSafeAreaWorldBounds();
    }

    private void Update()
    {
        move = input.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = move * moveSpeed;

        if (clampToSafeArea)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, worldMin.x + paddingX, worldMax.x - paddingX);
            pos.y = Mathf.Clamp(pos.y, worldMin.y + paddingY, worldMax.y - paddingY);
            transform.position = pos;
        }
        HandleFlip();
    }

    private void CalculateSafeAreaWorldBounds()
    {
        if (mainCam == null) mainCam = Camera.main;
        if (mainCam == null) return;

        Rect safe = Screen.safeArea;

        // convert safe-area corners from screen pixels to world space
        Vector3 bottomLeft = mainCam.ScreenToWorldPoint(
            new Vector3(safe.xMin, safe.yMin, -mainCam.transform.position.z));
        Vector3 topRight = mainCam.ScreenToWorldPoint(
            new Vector3(safe.xMax, safe.yMax, -mainCam.transform.position.z));

        worldMin = bottomLeft;
        worldMax = topRight;
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