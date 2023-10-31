using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longerunit : MonoBehaviour
{
    public float detectionRange = 10.0f; // �÷��̾� ���� ����
    private Transform monster; // �÷��̾��� Transform
  
    public float moveSpeed = 1.0f; // �̵� �ӵ�
    public float minAttackCooldown = 2.0f; // �ּ� ���� ��Ÿ��
    public float maxAttackCooldown = 5.0f; // �ִ� ���� ��Ÿ��
    private float currentAttackCooldown = 0.0f;
    private bool canAttack = true;
    private Animator animator;
    private float nextAttackTime = 0.0f;
    private System.Random random = new System.Random();
    private bool isActionInProgress = false;
    public float minX = -5.0f; // �ּ� X ��ġ
    public float maxX = 5.0f; // �ִ� X ��ġ
    public float rangexX = -5.0f;
    public float rangeXX = 5.0f;
    public GameObject arrowPrefab; // ȭ�� ������
    public float gravity = 9.81f; // �߷� ���ӵ�
    private Coroutine currentCoroutine;
    private string GodonX = "PlayerPositionGoX";
    private string GodonY = "PlayerPositionGoY";
    private string GodonZ = "PlayerPositionGoZ";
    private bool isMoving = false;
    private Vector3 TargetPosition;

    private void Start()
    {
        
        animator = GetComponent<Animator>();
        Vector3 position = LoadGoPosition();
        transform.position = position;
        Callmovewall();

    }


    private void Update()
    {
        SaveGoPosition(transform.position);

        if (monster == null)
        {
            GameObject monsterObject = GameObject.FindGameObjectWithTag("Enemy");
            if (monsterObject != null)
            {
                monster = monsterObject.transform;
            }
        }

        
        // �÷��̾� ���� ����

        if (monster != null)
        {
            float distanceToMonster = Vector3.Distance(transform.position, monster.position);

            if (distanceToMonster <= detectionRange)
            {
                Attack();
            }
        }
        if (isMoving)
        {
            float randomX = Random.Range(rangexX, rangeXX);
            float newScaleX = transform.localScale.x;

            // ��ǥ ��ġ�� ���� ��ġ�� ���Ͽ� ������ ����
            if (randomX < transform.position.x)
            {
                newScaleX = -Mathf.Abs(newScaleX); // �������� �̵�
            }
            else
            {
                newScaleX = Mathf.Abs(newScaleX); // ���������� �̵�
            }

            animator.SetBool("IsWalking", true);
            Vector3 TargetPosition = new Vector3(randomX, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, moveSpeed * Time.deltaTime);
            transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);

            if (Vector3.Distance(transform.position, TargetPosition) <= 0.1f)
            {
                isMoving = false;

                // ��ǥ�� ������ �� ������ ������ ����
                if (newScaleX < 0)
                {
                    newScaleX = Mathf.Abs(newScaleX);
                    transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
                }

                animator.SetBool("IsWalking", false); // ���⿡ ��� ��� ����
            }
        }


    }

    public void Callmovewall()
    {
        CalendarManager calendarManager = FindObjectOfType<CalendarManager>();
        if (calendarManager != null)
        {
            int currentHour = calendarManager.GetHour();
            if (currentHour >= 7)
            {

                Movewall();


            }
            else if( currentHour <= 7)
            {
                PerformRandomAction();

                Debug.Log("Dd");
            }
        }
    }

    public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            Vector3 direction = monster.position - transform.position;
            float distance = direction.magnitude;

            // ������ � ���
            if(distance < 5 && distance >= 4.6)
            { 

            animator.SetTrigger("Attack");
                float arrowSpeed = 0.5f;
            float arrowSpeedMultiplier = 1.0f; // �� ���� �����Ͽ� ȭ�� �ӵ��� ��� ������ ������ �� �ֽ��ϴ�.
            float adjustedArrowSpeed = arrowSpeed * (1.0f + distance * arrowSpeedMultiplier);
            float time = distance / adjustedArrowSpeed;
            float verticalSpeed = adjustedArrowSpeed - (gravity * time) / 2;

            Vector3 initialVelocity = direction.normalized * adjustedArrowSpeed;
            initialVelocity.y = verticalSpeed*3;

            // ȭ�� �߻�
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
            arrowRb.velocity = initialVelocity;

            // ���⿡ ���� ���� ������ ����
            Debug.Log("���Ͱ� �÷��̾ �����մϴ�!");
            currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
            nextAttackTime = Time.time + currentAttackCooldown;
            }
            else if (distance < 4.6 && distance >= 4)
            {
                animator.SetTrigger("Attack");
                float arrowSpeed = 0.52f;
                float arrowSpeedMultiplier = 1.0f; // �� ���� �����Ͽ� ȭ�� �ӵ��� ��� ������ ������ �� �ֽ��ϴ�.
                float adjustedArrowSpeed = arrowSpeed * (1.0f + distance * arrowSpeedMultiplier);
                float time = distance / adjustedArrowSpeed;
                float verticalSpeed = adjustedArrowSpeed - (gravity * time) / 2;

                Vector3 initialVelocity = direction.normalized * adjustedArrowSpeed;
                initialVelocity.y = verticalSpeed * 3;

                // ȭ�� �߻�
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
                arrowRb.velocity = initialVelocity;

                // ���⿡ ���� ���� ������ ����
                Debug.Log("���Ͱ� �÷��̾ �����մϴ�!");
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                nextAttackTime = Time.time + currentAttackCooldown;
            }
            else if (distance < 4 && distance >= 3.6)
            {
                animator.SetTrigger("Attack");
                float arrowSpeed = 0.54f;
                float arrowSpeedMultiplier = 1.0f; // �� ���� �����Ͽ� ȭ�� �ӵ��� ��� ������ ������ �� �ֽ��ϴ�.
                float adjustedArrowSpeed = arrowSpeed * (1.0f + distance * arrowSpeedMultiplier);
                float time = distance / adjustedArrowSpeed;
                float verticalSpeed = adjustedArrowSpeed - (gravity * time) / 2;

                Vector3 initialVelocity = direction.normalized * adjustedArrowSpeed;
                initialVelocity.y = verticalSpeed * 3;

                // ȭ�� �߻�
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
                arrowRb.velocity = initialVelocity;

                // ���⿡ ���� ���� ������ ����
                Debug.Log("���Ͱ� �÷��̾ �����մϴ�!");
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                nextAttackTime = Time.time + currentAttackCooldown;
            }
            else if (distance < 3.6 && distance >= 3.1)
            {
                animator.SetTrigger("Attack");
                float arrowSpeed = 0.54f;
                float arrowSpeedMultiplier = 1.0f; // �� ���� �����Ͽ� ȭ�� �ӵ��� ��� ������ ������ �� �ֽ��ϴ�.
                float adjustedArrowSpeed = arrowSpeed * (1.0f + distance * arrowSpeedMultiplier);
                float time = distance / adjustedArrowSpeed;
                float verticalSpeed = adjustedArrowSpeed - (gravity * time) / 2;

                Vector3 initialVelocity = direction.normalized * adjustedArrowSpeed;
                initialVelocity.y = verticalSpeed * 3;

                // ȭ�� �߻�
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
                arrowRb.velocity = initialVelocity;

                // ���⿡ ���� ���� ������ ����
                Debug.Log("���Ͱ� �÷��̾ �����մϴ�!");
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                nextAttackTime = Time.time + currentAttackCooldown;
            }
            else if (distance < 3.1 && distance >= 2.7)
            {
                animator.SetTrigger("Attack");
                float arrowSpeed = 0.56f;
                float arrowSpeedMultiplier = 1.0f; // �� ���� �����Ͽ� ȭ�� �ӵ��� ��� ������ ������ �� �ֽ��ϴ�.
                float adjustedArrowSpeed = arrowSpeed * (1.0f + distance * arrowSpeedMultiplier);
                float time = distance / adjustedArrowSpeed;
                float verticalSpeed = adjustedArrowSpeed - (gravity * time) / 2;

                Vector3 initialVelocity = direction.normalized * adjustedArrowSpeed;
                initialVelocity.y = verticalSpeed * 3;

                // ȭ�� �߻�
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
                arrowRb.velocity = initialVelocity;

                // ���⿡ ���� ���� ������ ����
                Debug.Log("���Ͱ� �÷��̾ �����մϴ�!");
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                nextAttackTime = Time.time + currentAttackCooldown;
            }
            else if (distance < 2.7 && distance >= 2.3)
            {
                animator.SetTrigger("Attack");
                float arrowSpeed = 0.6f;
                float arrowSpeedMultiplier = 1.0f; // �� ���� �����Ͽ� ȭ�� �ӵ��� ��� ������ ������ �� �ֽ��ϴ�.
                float adjustedArrowSpeed = arrowSpeed * (1.0f + distance * arrowSpeedMultiplier);
                float time = distance / adjustedArrowSpeed;
                float verticalSpeed = adjustedArrowSpeed - (gravity * time) / 2;

                Vector3 initialVelocity = direction.normalized * adjustedArrowSpeed;
                initialVelocity.y = verticalSpeed * 3;

                // ȭ�� �߻�
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
                arrowRb.velocity = initialVelocity;

                // ���⿡ ���� ���� ������ ����
                Debug.Log("���Ͱ� �÷��̾ �����մϴ�!");
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                nextAttackTime = Time.time + currentAttackCooldown;
            }
            else if (distance < 2.3 )
            {
                animator.SetTrigger("Attack");
                float arrowSpeed = 0.63f;
                float arrowSpeedMultiplier = 1.0f; // �� ���� �����Ͽ� ȭ�� �ӵ��� ��� ������ ������ �� �ֽ��ϴ�.
                float adjustedArrowSpeed = arrowSpeed * (1.0f + distance * arrowSpeedMultiplier);
                float time = distance / adjustedArrowSpeed;
                float verticalSpeed = adjustedArrowSpeed - (gravity * time) / 2;

                Vector3 initialVelocity = direction.normalized * adjustedArrowSpeed;
                initialVelocity.y = verticalSpeed * 3;

                // ȭ�� �߻�
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
                arrowRb.velocity = initialVelocity;

                // ���⿡ ���� ���� ������ ����
                Debug.Log("���Ͱ� �÷��̾ �����մϴ�!");
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                nextAttackTime = Time.time + currentAttackCooldown;
            }
        }
    }


    private void Movewall()
    {
        // �̵� ���� ����
        isMoving = true;
    }



    private void PerformRandomAction()
    {
        Debug.Log("ddd");
        Vector3 currentPosition = transform.position;
        if (isActionInProgress)
        {
            
            return;
            Debug.Log("ama");
        }
        if (currentPosition.x >= minX && currentPosition.x <= maxX)
        {
            // ������ ���� 1~3�� ����
            int randomNumber = random.Next(1, 7);

            // ���ڿ� ���� �ൿ�� ����
            switch (randomNumber)
            {
                case 1:
                    // 1�� ������ ���� �ൿ (��)
                    StartCoroutine(PerformActionA());
                    break;
                case 2:
                    // 2�� ������ ���� �ൿ (��)
                    StartCoroutine(PerformActionB());
                    break;
                case 3:
                    // 3�� ������ ���� �ൿ (��)
                    StartCoroutine(PerformActionC());
                    break;
                case 4:
                    // 2�� ������ ���� �ൿ (��)
                    StartCoroutine(PerformActionD());
                    break;
                case 5:
                    // 2�� ������ ���� �ൿ (��)
                    StartCoroutine(PerformActionE());
                    break;
                case 6:
                    // 2�� ������ ���� �ൿ (��)
                    StartCoroutine(PerformActionF());
                    break;
                default:
                    Debug.Log("����ġ ���� ���ڰ� ���Խ��ϴ�.");
                    break;
            }
        }
        else if (currentPosition.x <= minX)
        {
            int randomNumber = random.Next(1, 4);

            switch (randomNumber)
            {
                case 1:
                    StartCoroutine(PerformActionA());
                    break;
                case 2:
                    StartCoroutine(PerformActionC());
                    break;
                case 3:
                    StartCoroutine(PerformActionE());
                    break;
            }
        }
        else if (currentPosition.x >= maxX)
        {
            int randomNumber = random.Next(1, 4);
            switch (randomNumber)
            {
                case 1:
                    StartCoroutine(PerformActionB());
                    break;
                case 2:
                    StartCoroutine(PerformActionD());
                    break;
                case 3:
                    StartCoroutine(PerformActionF());
                    break;
            }
        }



    }

    private IEnumerator WaitForAnimation(string animationName)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) || animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
    }

    private IEnumerator PerformActionA()
    {
        isActionInProgress = true;
        Debug.Log("�ൿ �� ����");

        // �������� 2~5�� ������ �ð��� ����
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 0.7f;
        // ���������� �̵� �߿� �ȴ� �ִϸ��̼� ���
        animator.SetBool("IsWalking", true);
        transform.localScale = new Vector3(3, 3, 1);

        // ���������� �̵�
        float moveSpeed = 1.0f; // �̵� �ӵ�
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.right * moveDistance; // ���������� 5��ŭ �̵�

        while (elapsedTime < moveDuration)
        {
            // �̵� ����: ���� ��ġ���� ��ǥ ��ġ���� �̵�
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);

            // �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // �̵��� ������ �ȴ� �ִϸ��̼� ����
        animator.SetBool("IsWalking", false);

        // �� �ִϸ��̼��� ���� (Animator ������Ʈ�� Ʈ���� ����)
        animator.SetTrigger("Head");

        // �� �ִϸ��̼��� ������ ��ٸ� �� �ൿ�� ���� ������ ǥ��
        isActionInProgress = false;


    }

    private IEnumerator PerformActionB()
    {
        isActionInProgress = true;
        Debug.Log("�ൿ �� ����");

        // �������� 2~5�� ������ �ð��� ����
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 0.7f;

        // ���������� �̵� �߿� �ȴ� �ִϸ��̼� ���
        animator.SetBool("IsWalking", true);
        transform.localScale = new Vector3(-3, 3, 1);
        // ���������� �̵�
        float moveSpeed = 1.0f; // �̵� �ӵ�
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.left * moveDistance; // �������� 5��ŭ �̵�

        while (elapsedTime < moveDuration)
        {
            // �̵� ����: ���� ��ġ���� ��ǥ ��ġ���� �̵�
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);

            // �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // �̵��� ������ �ȴ� �ִϸ��̼� ����
        animator.SetBool("IsWalking", false);

        // �� �ִϸ��̼��� ���� (Animator ������Ʈ�� Ʈ���� ����)
        animator.SetTrigger("Head");

        // �� �ִϸ��̼��� ������ ��ٸ� �� �ൿ�� ���� ������ ǥ��
        isActionInProgress = false;


    }

    private IEnumerator PerformActionC()
    {
        isActionInProgress = true;
        Debug.Log("�ൿ �� ����");

        // �������� 2~5�� ������ �ð��� ����
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 0.7f;

        // ���������� �̵� �߿� �ȴ� �ִϸ��̼� ���
        animator.SetBool("IsWalking", true);
        transform.localScale = new Vector3(3, 3, 1);

        // ���������� �̵�
        float moveSpeed = 1.0f; // �̵� �ӵ�
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.right * moveDistance; // ���������� 5��ŭ �̵�

        while (elapsedTime < moveDuration)
        {
            // �̵� ����: ���� ��ġ���� ��ǥ ��ġ���� �̵�
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);

            // �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // �̵��� ������ �ȴ� �ִϸ��̼� ����
        animator.SetBool("IsWalking", false);

        // �� �ִϸ��̼��� ���� (Animator ������Ʈ�� Ʈ���� ����)
        animator.SetTrigger("Health");

        // �� �ִϸ��̼��� ������ ��ٸ� �� �ൿ�� ���� ������ ǥ��
        isActionInProgress = false;


    }



    private IEnumerator PerformActionD()
    {
        isActionInProgress = true;
        Debug.Log("�ൿ �� ����");

        // �������� 2~5�� ������ �ð��� ����
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 0.7f;

        // ���������� �̵� �߿� �ȴ� �ִϸ��̼� ���
        animator.SetBool("IsWalking", true);
        transform.localScale = new Vector3(-3, 3, 1);

        // ���������� �̵�
        float moveSpeed = 1.0f; // �̵� �ӵ�
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.left * moveDistance; // ���������� 5��ŭ �̵�

        while (elapsedTime < moveDuration)
        {
            // �̵� ����: ���� ��ġ���� ��ǥ ��ġ���� �̵�
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);

            // �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // �̵��� ������ �ȴ� �ִϸ��̼� ����
        animator.SetBool("IsWalking", false);

        // �� �ִϸ��̼��� ���� (Animator ������Ʈ�� Ʈ���� ����)
        animator.SetTrigger("Health");

        // �� �ִϸ��̼��� ������ ��ٸ� �� �ൿ�� ���� ������ ǥ��
        isActionInProgress = false;


    }

    private IEnumerator PerformActionE()
    {
        isActionInProgress = true;
        Debug.Log("�ൿ �� ����");

        // �������� 2~5�� ������ �ð��� ����
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 0.7f;

        // ���������� �̵� �߿� �ȴ� �ִϸ��̼� ���
        animator.SetBool("IsWalking", true);
        transform.localScale = new Vector3(3, 3, 1);

        // ���������� �̵�
        float moveSpeed = 1.0f; // �̵� �ӵ�
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.right * moveDistance; // ���������� 5��ŭ �̵�

        while (elapsedTime < moveDuration)
        {
            // �̵� ����: ���� ��ġ���� ��ǥ ��ġ���� �̵�
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);

            // �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // �̵��� ������ �ȴ� �ִϸ��̼� ����
        animator.SetBool("IsWalking", false);

        // �� �ִϸ��̼��� ���� (Animator ������Ʈ�� Ʈ���� ����)
        animator.SetTrigger("Mix");

        // �� �ִϸ��̼��� ������ ��ٸ� �� �ൿ�� ���� ������ ǥ��
        isActionInProgress = false;

    }

    private IEnumerator PerformActionF()
    {
        isActionInProgress = true;
        Debug.Log("�ൿ �� ����");

        // �������� 2~5�� ������ �ð��� ����
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 0.7f;
        // ���������� �̵� �߿� �ȴ� �ִϸ��̼� ���
        animator.SetBool("IsWalking", true);
        transform.localScale = new Vector3(-3, 3, 1);

        // ���������� �̵�
        float moveSpeed = 1.0f; // �̵� �ӵ�
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.left * moveDistance; // ���������� 5��ŭ �̵�

        while (elapsedTime < moveDuration)
        {
            // �̵� ����: ���� ��ġ���� ��ǥ ��ġ���� �̵�
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);

            // �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // �̵��� ������ �ȴ� �ִϸ��̼� ����
        animator.SetBool("IsWalking", false);

        // �� �ִϸ��̼��� ���� (Animator ������Ʈ�� Ʈ���� ����)
        animator.SetTrigger("Mix");

        // �� �ִϸ��̼��� ������ ��ٸ� �� �ൿ�� ���� ������ ǥ��
        isActionInProgress = false;


    }

    private void OnHeadAnimationEnd()
    {
        isActionInProgress = false;
        PerformRandomAction();
    }

    public void SaveGoPosition(Vector3 position)
    {


        PlayerPrefs.SetFloat(GodonX, position.x);
        PlayerPrefs.SetFloat(GodonY, position.y);
        PlayerPrefs.SetFloat(GodonZ, position.z);
        PlayerPrefs.Save();

    }
    public Vector3 LoadGoPosition()
    {
        float x = PlayerPrefs.GetFloat(GodonX, 0f); // 0f�� �⺻ ��ġ
        float y = PlayerPrefs.GetFloat(GodonY, -0.63f);
        float z = PlayerPrefs.GetFloat(GodonZ, 0f);
        return new Vector3(x, y, z);
    }

}