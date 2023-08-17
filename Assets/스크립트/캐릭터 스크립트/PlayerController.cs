using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isGrounded = true;
    public AudioClip jumpClip;
    private AudioSource playerAudio;
    public float attackCooldown = 0.5f;
    private float nextAttackTime = 0f;
    private bool isAttacking = false;
    public AudioClip attackClip;
    public Transform attackPoint;
    public float attackRange = 5f;
    public LayerMask enemyLayers;


    private CharacterHealth characterHealth; // CharacterHealth 스크립트를 참조할 변수 추가

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        enemyLayers = LayerMask.GetMask("Enemy");
        characterHealth = GetComponent<CharacterHealth>(); // CharacterHealth 스크립트 할당
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire1") && !isAttacking && Time.time >= nextAttackTime)
        {
            Attack();
        }
        if (isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            isAttacking = false;
        }
        Move(horizontalInput);
        UpdateAnimatorParameters(horizontalInput);

        // 현재 체력 검사
        if (characterHealth.currentHealth <= 0) // CharacterHealth 스크립트의 currentHealth 사용
        {
            Die();
        }
    }
    private void Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        playerAudio.PlayOneShot(attackClip);
        nextAttackTime = Time.time + attackCooldown;
        DealDamage();
    }

    private void Move(float horizontalInput)
    {
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        if ((horizontalInput > 0 && !isFacingRight) || (horizontalInput < 0 && isFacingRight))
        {
            FlipCharacter();
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isGrounded = false;
        playerAudio.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
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
        animator.SetBool("IsGrounded", isGrounded);
    }



    private void Die()
    {
        Debug.Log("Die animation triggered!");
        animator.SetTrigger("Die");
    }
    public void DealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            MonsterHealth monsterHealth = enemy.GetComponent<MonsterHealth>();
            if (monsterHealth != null)
            {
                monsterHealth.TakeDamage(30); // 10은 데미지량으로 조절 가능
            }
            BossMonsterHealth BossmonsterHealth = enemy.GetComponent<BossMonsterHealth>();
            if (BossmonsterHealth != null)
            {
                BossmonsterHealth.TakeDamage(10); // 10은 데미지량으로 조절 가능
            }
        }
    }
}