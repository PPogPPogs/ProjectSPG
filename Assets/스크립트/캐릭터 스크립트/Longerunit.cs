using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longerunit : MonoBehaviour
{
    public float detectionRange = 10.0f; // 플레이어 감지 범위
    private Transform monster; // 플레이어의 Transform
  
    public float moveSpeed = 1.0f; // 이동 속도
    public float minAttackCooldown = 2.0f; // 최소 공격 쿨타임
    public float maxAttackCooldown = 5.0f; // 최대 공격 쿨타임
    private float currentAttackCooldown = 0.0f;
    private bool canAttack = true;
    private Animator animator;
    private float nextAttackTime = 0.0f;
    private System.Random random = new System.Random();
    private bool isActionInProgress = false;
    public float minX = -5.0f; // 최소 X 위치
    public float maxX = 5.0f; // 최대 X 위치
    public float rangexX = -5.0f;
    public float rangeXX = 5.0f;
    public GameObject arrowPrefab; // 화살 프리팹
    public float gravity = 9.81f; // 중력 가속도
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

        
        // 플레이어 감지 로직

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

            // 목표 위치와 현재 위치를 비교하여 방향을 설정
            if (randomX < transform.position.x)
            {
                newScaleX = -Mathf.Abs(newScaleX); // 왼쪽으로 이동
            }
            else
            {
                newScaleX = Mathf.Abs(newScaleX); // 오른쪽으로 이동
            }

            animator.SetBool("IsWalking", true);
            Vector3 TargetPosition = new Vector3(randomX, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, moveSpeed * Time.deltaTime);
            transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);

            if (Vector3.Distance(transform.position, TargetPosition) <= 0.1f)
            {
                isMoving = false;

                // 목표에 도달한 후 스케일 방향을 조정
                if (newScaleX < 0)
                {
                    newScaleX = Mathf.Abs(newScaleX);
                    transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
                }

                animator.SetBool("IsWalking", false); // 여기에 대기 모션 변경
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

            // 포물선 운동 계산
            if(distance < 5 && distance >= 4.6)
            { 

            animator.SetTrigger("Attack");
                float arrowSpeed = 0.5f;
            float arrowSpeedMultiplier = 1.0f; // 이 값을 조절하여 화살 속도의 비례 정도를 변경할 수 있습니다.
            float adjustedArrowSpeed = arrowSpeed * (1.0f + distance * arrowSpeedMultiplier);
            float time = distance / adjustedArrowSpeed;
            float verticalSpeed = adjustedArrowSpeed - (gravity * time) / 2;

            Vector3 initialVelocity = direction.normalized * adjustedArrowSpeed;
            initialVelocity.y = verticalSpeed*3;

            // 화살 발사
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
            arrowRb.velocity = initialVelocity;

            // 여기에 실제 공격 로직을 구현
            Debug.Log("몬스터가 플레이어를 공격합니다!");
            currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
            nextAttackTime = Time.time + currentAttackCooldown;
            }
            else if (distance < 4.6 && distance >= 4)
            {
                animator.SetTrigger("Attack");
                float arrowSpeed = 0.52f;
                float arrowSpeedMultiplier = 1.0f; // 이 값을 조절하여 화살 속도의 비례 정도를 변경할 수 있습니다.
                float adjustedArrowSpeed = arrowSpeed * (1.0f + distance * arrowSpeedMultiplier);
                float time = distance / adjustedArrowSpeed;
                float verticalSpeed = adjustedArrowSpeed - (gravity * time) / 2;

                Vector3 initialVelocity = direction.normalized * adjustedArrowSpeed;
                initialVelocity.y = verticalSpeed * 3;

                // 화살 발사
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
                arrowRb.velocity = initialVelocity;

                // 여기에 실제 공격 로직을 구현
                Debug.Log("몬스터가 플레이어를 공격합니다!");
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                nextAttackTime = Time.time + currentAttackCooldown;
            }
            else if (distance < 4 && distance >= 3.6)
            {
                animator.SetTrigger("Attack");
                float arrowSpeed = 0.54f;
                float arrowSpeedMultiplier = 1.0f; // 이 값을 조절하여 화살 속도의 비례 정도를 변경할 수 있습니다.
                float adjustedArrowSpeed = arrowSpeed * (1.0f + distance * arrowSpeedMultiplier);
                float time = distance / adjustedArrowSpeed;
                float verticalSpeed = adjustedArrowSpeed - (gravity * time) / 2;

                Vector3 initialVelocity = direction.normalized * adjustedArrowSpeed;
                initialVelocity.y = verticalSpeed * 3;

                // 화살 발사
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
                arrowRb.velocity = initialVelocity;

                // 여기에 실제 공격 로직을 구현
                Debug.Log("몬스터가 플레이어를 공격합니다!");
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                nextAttackTime = Time.time + currentAttackCooldown;
            }
            else if (distance < 3.6 && distance >= 3.1)
            {
                animator.SetTrigger("Attack");
                float arrowSpeed = 0.54f;
                float arrowSpeedMultiplier = 1.0f; // 이 값을 조절하여 화살 속도의 비례 정도를 변경할 수 있습니다.
                float adjustedArrowSpeed = arrowSpeed * (1.0f + distance * arrowSpeedMultiplier);
                float time = distance / adjustedArrowSpeed;
                float verticalSpeed = adjustedArrowSpeed - (gravity * time) / 2;

                Vector3 initialVelocity = direction.normalized * adjustedArrowSpeed;
                initialVelocity.y = verticalSpeed * 3;

                // 화살 발사
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
                arrowRb.velocity = initialVelocity;

                // 여기에 실제 공격 로직을 구현
                Debug.Log("몬스터가 플레이어를 공격합니다!");
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                nextAttackTime = Time.time + currentAttackCooldown;
            }
            else if (distance < 3.1 && distance >= 2.7)
            {
                animator.SetTrigger("Attack");
                float arrowSpeed = 0.56f;
                float arrowSpeedMultiplier = 1.0f; // 이 값을 조절하여 화살 속도의 비례 정도를 변경할 수 있습니다.
                float adjustedArrowSpeed = arrowSpeed * (1.0f + distance * arrowSpeedMultiplier);
                float time = distance / adjustedArrowSpeed;
                float verticalSpeed = adjustedArrowSpeed - (gravity * time) / 2;

                Vector3 initialVelocity = direction.normalized * adjustedArrowSpeed;
                initialVelocity.y = verticalSpeed * 3;

                // 화살 발사
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
                arrowRb.velocity = initialVelocity;

                // 여기에 실제 공격 로직을 구현
                Debug.Log("몬스터가 플레이어를 공격합니다!");
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                nextAttackTime = Time.time + currentAttackCooldown;
            }
            else if (distance < 2.7 && distance >= 2.3)
            {
                animator.SetTrigger("Attack");
                float arrowSpeed = 0.6f;
                float arrowSpeedMultiplier = 1.0f; // 이 값을 조절하여 화살 속도의 비례 정도를 변경할 수 있습니다.
                float adjustedArrowSpeed = arrowSpeed * (1.0f + distance * arrowSpeedMultiplier);
                float time = distance / adjustedArrowSpeed;
                float verticalSpeed = adjustedArrowSpeed - (gravity * time) / 2;

                Vector3 initialVelocity = direction.normalized * adjustedArrowSpeed;
                initialVelocity.y = verticalSpeed * 3;

                // 화살 발사
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
                arrowRb.velocity = initialVelocity;

                // 여기에 실제 공격 로직을 구현
                Debug.Log("몬스터가 플레이어를 공격합니다!");
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                nextAttackTime = Time.time + currentAttackCooldown;
            }
            else if (distance < 2.3 )
            {
                animator.SetTrigger("Attack");
                float arrowSpeed = 0.63f;
                float arrowSpeedMultiplier = 1.0f; // 이 값을 조절하여 화살 속도의 비례 정도를 변경할 수 있습니다.
                float adjustedArrowSpeed = arrowSpeed * (1.0f + distance * arrowSpeedMultiplier);
                float time = distance / adjustedArrowSpeed;
                float verticalSpeed = adjustedArrowSpeed - (gravity * time) / 2;

                Vector3 initialVelocity = direction.normalized * adjustedArrowSpeed;
                initialVelocity.y = verticalSpeed * 3;

                // 화살 발사
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
                arrowRb.velocity = initialVelocity;

                // 여기에 실제 공격 로직을 구현
                Debug.Log("몬스터가 플레이어를 공격합니다!");
                currentAttackCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
                nextAttackTime = Time.time + currentAttackCooldown;
            }
        }
    }


    private void Movewall()
    {
        // 이동 로직 구현
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
            // 무작위 숫자 1~3을 생성
            int randomNumber = random.Next(1, 7);

            // 숫자에 따라 행동을 수행
            switch (randomNumber)
            {
                case 1:
                    // 1이 나왔을 때의 행동 (ㄱ)
                    StartCoroutine(PerformActionA());
                    break;
                case 2:
                    // 2가 나왔을 때의 행동 (ㄴ)
                    StartCoroutine(PerformActionB());
                    break;
                case 3:
                    // 3이 나왔을 때의 행동 (ㄷ)
                    StartCoroutine(PerformActionC());
                    break;
                case 4:
                    // 2가 나왔을 때의 행동 (ㄴ)
                    StartCoroutine(PerformActionD());
                    break;
                case 5:
                    // 2가 나왔을 때의 행동 (ㄴ)
                    StartCoroutine(PerformActionE());
                    break;
                case 6:
                    // 2가 나왔을 때의 행동 (ㄴ)
                    StartCoroutine(PerformActionF());
                    break;
                default:
                    Debug.Log("예기치 않은 숫자가 나왔습니다.");
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
        Debug.Log("행동 ㄱ 수행");

        // 무작위로 2~5초 사이의 시간을 생성
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 0.7f;
        // 오른쪽으로 이동 중에 걷는 애니메이션 재생
        animator.SetBool("IsWalking", true);
        transform.localScale = new Vector3(3, 3, 1);

        // 오른쪽으로 이동
        float moveSpeed = 1.0f; // 이동 속도
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.right * moveDistance; // 오른쪽으로 5만큼 이동

        while (elapsedTime < moveDuration)
        {
            // 이동 로직: 현재 위치에서 목표 위치까지 이동
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);

            // 시간 업데이트
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 이동이 끝나면 걷는 애니메이션 중지
        animator.SetBool("IsWalking", false);

        // ㄱ 애니메이션을 시작 (Animator 컴포넌트의 트리거 설정)
        animator.SetTrigger("Head");

        // ㄱ 애니메이션이 끝나길 기다린 후 행동이 끝난 것으로 표시
        isActionInProgress = false;


    }

    private IEnumerator PerformActionB()
    {
        isActionInProgress = true;
        Debug.Log("행동 ㄱ 수행");

        // 무작위로 2~5초 사이의 시간을 생성
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 0.7f;

        // 오른쪽으로 이동 중에 걷는 애니메이션 재생
        animator.SetBool("IsWalking", true);
        transform.localScale = new Vector3(-3, 3, 1);
        // 오른쪽으로 이동
        float moveSpeed = 1.0f; // 이동 속도
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.left * moveDistance; // 왼쪽으로 5만큼 이동

        while (elapsedTime < moveDuration)
        {
            // 이동 로직: 현재 위치에서 목표 위치까지 이동
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);

            // 시간 업데이트
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 이동이 끝나면 걷는 애니메이션 중지
        animator.SetBool("IsWalking", false);

        // ㄱ 애니메이션을 시작 (Animator 컴포넌트의 트리거 설정)
        animator.SetTrigger("Head");

        // ㄱ 애니메이션이 끝나길 기다린 후 행동이 끝난 것으로 표시
        isActionInProgress = false;


    }

    private IEnumerator PerformActionC()
    {
        isActionInProgress = true;
        Debug.Log("행동 ㄷ 수행");

        // 무작위로 2~5초 사이의 시간을 생성
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 0.7f;

        // 오른쪽으로 이동 중에 걷는 애니메이션 재생
        animator.SetBool("IsWalking", true);
        transform.localScale = new Vector3(3, 3, 1);

        // 오른쪽으로 이동
        float moveSpeed = 1.0f; // 이동 속도
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.right * moveDistance; // 오른쪽으로 5만큼 이동

        while (elapsedTime < moveDuration)
        {
            // 이동 로직: 현재 위치에서 목표 위치까지 이동
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);

            // 시간 업데이트
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 이동이 끝나면 걷는 애니메이션 중지
        animator.SetBool("IsWalking", false);

        // ㄱ 애니메이션을 시작 (Animator 컴포넌트의 트리거 설정)
        animator.SetTrigger("Health");

        // ㄱ 애니메이션이 끝나길 기다린 후 행동이 끝난 것으로 표시
        isActionInProgress = false;


    }



    private IEnumerator PerformActionD()
    {
        isActionInProgress = true;
        Debug.Log("행동 ㄹ 수행");

        // 무작위로 2~5초 사이의 시간을 생성
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 0.7f;

        // 오른쪽으로 이동 중에 걷는 애니메이션 재생
        animator.SetBool("IsWalking", true);
        transform.localScale = new Vector3(-3, 3, 1);

        // 오른쪽으로 이동
        float moveSpeed = 1.0f; // 이동 속도
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.left * moveDistance; // 오른쪽으로 5만큼 이동

        while (elapsedTime < moveDuration)
        {
            // 이동 로직: 현재 위치에서 목표 위치까지 이동
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);

            // 시간 업데이트
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 이동이 끝나면 걷는 애니메이션 중지
        animator.SetBool("IsWalking", false);

        // ㄱ 애니메이션을 시작 (Animator 컴포넌트의 트리거 설정)
        animator.SetTrigger("Health");

        // ㄱ 애니메이션이 끝나길 기다린 후 행동이 끝난 것으로 표시
        isActionInProgress = false;


    }

    private IEnumerator PerformActionE()
    {
        isActionInProgress = true;
        Debug.Log("행동 ㅁ 수행");

        // 무작위로 2~5초 사이의 시간을 생성
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 0.7f;

        // 오른쪽으로 이동 중에 걷는 애니메이션 재생
        animator.SetBool("IsWalking", true);
        transform.localScale = new Vector3(3, 3, 1);

        // 오른쪽으로 이동
        float moveSpeed = 1.0f; // 이동 속도
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.right * moveDistance; // 오른쪽으로 5만큼 이동

        while (elapsedTime < moveDuration)
        {
            // 이동 로직: 현재 위치에서 목표 위치까지 이동
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);

            // 시간 업데이트
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 이동이 끝나면 걷는 애니메이션 중지
        animator.SetBool("IsWalking", false);

        // ㄱ 애니메이션을 시작 (Animator 컴포넌트의 트리거 설정)
        animator.SetTrigger("Mix");

        // ㄱ 애니메이션이 끝나길 기다린 후 행동이 끝난 것으로 표시
        isActionInProgress = false;

    }

    private IEnumerator PerformActionF()
    {
        isActionInProgress = true;
        Debug.Log("행동 ㅂ 수행");

        // 무작위로 2~5초 사이의 시간을 생성
        float moveDistance = UnityEngine.Random.Range(3.0f, 6.0f);
        float moveDuration = moveDistance / 0.7f;
        // 오른쪽으로 이동 중에 걷는 애니메이션 재생
        animator.SetBool("IsWalking", true);
        transform.localScale = new Vector3(-3, 3, 1);

        // 오른쪽으로 이동
        float moveSpeed = 1.0f; // 이동 속도
        float elapsedTime = 0f;

        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + Vector3.left * moveDistance; // 오른쪽으로 5만큼 이동

        while (elapsedTime < moveDuration)
        {
            // 이동 로직: 현재 위치에서 목표 위치까지 이동
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);

            // 시간 업데이트
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 이동이 끝나면 걷는 애니메이션 중지
        animator.SetBool("IsWalking", false);

        // ㄱ 애니메이션을 시작 (Animator 컴포넌트의 트리거 설정)
        animator.SetTrigger("Mix");

        // ㄱ 애니메이션이 끝나길 기다린 후 행동이 끝난 것으로 표시
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
        float x = PlayerPrefs.GetFloat(GodonX, 0f); // 0f는 기본 위치
        float y = PlayerPrefs.GetFloat(GodonY, -0.63f);
        float z = PlayerPrefs.GetFloat(GodonZ, 0f);
        return new Vector3(x, y, z);
    }

}