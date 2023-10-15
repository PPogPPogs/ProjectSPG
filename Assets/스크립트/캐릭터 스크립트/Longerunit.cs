using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longerunit : MonoBehaviour
{
    public float detectionRange = 10.0f; // �÷��̾� ���� ����
    private Transform monster; // �÷��̾��� Transform
    public Transform targetPosition; // �̵���ų ��ǥ ��ġ�� ������ Ʈ������
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


    private void Start()
    {
        animator = GetComponent<Animator>();


    }


    private void Update()
    {
        

        if (monster == null)
        {
            GameObject monsterObject = GameObject.FindGameObjectWithTag("Enemy");
            if (monsterObject != null)
            {
                monster = monsterObject.transform;
            }
        }

        CalendarManager calendarManager = FindObjectOfType<CalendarManager>();
        if (calendarManager != null)
        {
            int currentHour = calendarManager.GetHour();
            if (currentHour == 17)
            {
                Movewall();
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
        else
        {
            PerformRandomAction();
        }
    }


    private void Attack()
        {
            if (Time.time >= nextAttackTime)
            {
                animator.SetTrigger("Attack");
                // ���⿡ ���� ���� ������ ����
                Debug.Log("���Ͱ� �÷��̾ �����մϴ�!");
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                nextAttackTime = Time.time + currentAttackCooldown;
            } 
        }

    private void Movewall()
    {
        // �̵� ���� ����
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
    }



    private void PerformRandomAction()
    {
        Vector3 currentPosition = transform.position;
        if (isActionInProgress)
        {
            Debug.Log("�̹� �۾� ���Դϴ�.");
            return;
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
        else if(currentPosition.x <= minX)
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
        else if(currentPosition.x >= maxX)
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
        float moveDuration = moveDistance / 1.0f;
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
        yield return StartCoroutine(WaitForAnimation("Head"));

      
    }

    private IEnumerator PerformActionB()
    {
        isActionInProgress = true;
        Debug.Log("�ൿ �� ����");

        // �������� 2~5�� ������ �ð��� ����
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 1.0f;

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
        yield return StartCoroutine(WaitForAnimation("Head"));

        
    }

    private IEnumerator PerformActionC()
    {
        isActionInProgress = true;
        Debug.Log("�ൿ �� ����");

        // �������� 2~5�� ������ �ð��� ����
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 1.0f;

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
        yield return StartCoroutine(WaitForAnimation("Health"));

       
    }



    private IEnumerator PerformActionD()
    {
        isActionInProgress = true;
        Debug.Log("�ൿ �� ����");

        // �������� 2~5�� ������ �ð��� ����
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 1.0f;

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
        yield return StartCoroutine(WaitForAnimation("Health"));

       
    }

    private IEnumerator PerformActionE()
    {
        isActionInProgress = true;
        Debug.Log("�ൿ �� ����");

        // �������� 2~5�� ������ �ð��� ����
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 1.0f;

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
        yield return StartCoroutine(WaitForAnimation("Mix"));

        
    }

    private IEnumerator PerformActionF()
    {
        isActionInProgress = true;
        Debug.Log("�ൿ �� ����");

        // �������� 2~5�� ������ �ð��� ����
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 1.0f;
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
        yield return StartCoroutine(WaitForAnimation("Mix"));

        
    }

    private void OnHeadAnimationEnd()
    {
        isActionInProgress = false;
        PerformRandomAction();
    }



}

