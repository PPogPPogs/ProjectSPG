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
            animator.SetBool("IsAiming", true); // "IsAiming"�� �Ҹ��� �Ű� �����Դϴ�.
        }

        // ���콺 ������ ��ư�� ���� �ִϸ��̼� ����
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("IsAiming", false); // "IsAiming"�� false�� �����Ͽ� �ִϸ��̼� ����
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