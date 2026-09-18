using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;

    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float downSpeed = 8f;

    private bool isJump = false;

    // --- コヨーテタイム用の設定 ---
    [SerializeField] private float coyoteTime = 0.15f; // 地面から離れてもジャンプを受け付ける時間（秒）
    private float coyoteTimer = 0f;                    // タイマー用

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 地面にいる、またはコヨーテタイム内であればタイマーをリセット
        if (!isJump)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            // 空中にいる間はタイマーを減らしていく
            coyoteTimer -= Time.deltaTime;
        }
    }

    public void OnJump(InputValue value)
    {
        // 押された瞬間 かつ （地面にいる OR コヨーテタイムの残り時間が残っている）
        if (value.isPressed && (!isJump || coyoteTimer > 0f))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);

            // 2回連続でジャンプしないようにタイマーを即座にゼロにする
            coyoteTimer = 0f;
            isJump = true;
        }
    }

    public void OnDown(InputValue value)
    {
        if (value.isPressed && isJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -downSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = true;
        }
    }
}