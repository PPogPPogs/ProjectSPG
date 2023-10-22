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
    private string PlayerPrefsKeyX = "PlayerPositionX";
    private string PlayerPrefsKeyY = "PlayerPositionY";
    private string PlayerPrefsKeyZ = "PlayerPositionZ";
    public float attackRange = 5f;
    public LayerMask enemyLayers;
    private bool isBossMonster = false;
   


    private CharacterHealth characterHealth; // CharacterHealth 스크립트를 참조할 변수 추가

    private void Awake()
    {
        
        Vector3 position = LoadPlayerPosition();
        transform.position = position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        enemyLayers = LayerMask.GetMask("Enemy");
        characterHealth = GetComponent<CharacterHealth>(); // CharacterHealth 스크립트 할당
    }

    private void Update()
    {


        SavePlayerPosition(transform.position);

        float horizontalInput = Input.GetAxisRaw("Horizontal");

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

    public bool GetFlipX()
    {
        return isFacingRight;
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

    public void SavePlayerPosition(Vector3 position)
    {
        CalendarManager calendarManager = FindObjectOfType<CalendarManager>();
        if (!isBossMonster)
        {

            if (calendarManager != null)
            {
                int currentHour = calendarManager.GetHour();
                if (currentHour <= 17)
                {
                    PlayerPrefs.SetFloat(PlayerPrefsKeyX, position.x);
                    PlayerPrefs.SetFloat(PlayerPrefsKeyY, position.y);
                    PlayerPrefs.SetFloat(PlayerPrefsKeyZ, position.z);
                    PlayerPrefs.Save();
                   
                }
            }
        }
    }

    public Vector3 LoadPlayerPosition()
    {
        float x = PlayerPrefs.GetFloat(PlayerPrefsKeyX, 0f); // 0f는 기본 위치
        float y = PlayerPrefs.GetFloat(PlayerPrefsKeyY, -0.59f);
        float z = PlayerPrefs.GetFloat(PlayerPrefsKeyZ, 0f);
        return new Vector3(x, y, z);
    }
}