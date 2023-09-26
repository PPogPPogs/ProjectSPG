using UnityEngine;

public class HandController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }


    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Move(horizontalInput);
        UpdateAnimatorParameters(horizontalInput);
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("IsAiming", true); // "IsAiming"은 불리언 매개 변수입니다.
        }

        // 마우스 오른쪽 버튼을 떼면 애니메이션 중지
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("IsAiming", false); // "IsAiming"을 false로 설정하여 애니메이션 중지
        }
    }

    private void Move(float horizontalInput)
    {
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

    }

    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void UpdateAnimatorParameters(float horizontalInput)
    {
        float speed = Mathf.Abs(horizontalInput) * moveSpeed;
        animator.SetFloat("Speed", speed);
        animator.SetBool("FlipX", !isFacingRight);
        animator.SetBool("IsRunning", speed != 0f);

    }
}