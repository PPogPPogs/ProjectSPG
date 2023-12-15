using UnityEngine;
using UnityEngine.UI;

public class Cannon1 : MonoBehaviour
{
    public float constructionTime = 10.0f; // 건물 건설 시간(초)
    public float constructionTimePercent = 1.0f; // 건물 건설 시간 배율(조종값)
    private float realcunstructionTime = 1.0f;
    private bool isUnderConstruction = false;
    private float constructionProgress = 0.0f;
    public Slider constructionBar;
    public GameObject Slider;
    private bool isConstruction = false;
    private bool isConstructioning = false;
    public Vector2 spawnPosition = new Vector2(-16.0f, -0.6f);// 저스틴 집 좌표
    public GameObject Cannon1Prefab;
    public GameObject Cannon2Prefab;

    public GameObject arrowPrefab; // 화살 프리팹
    public float gravity = 9.81f; // 중력 가속도
    private float nextAttackTime = 0.0f;
    private float currentAttackCooldown = 3.0f;
    private Animator animator;
    public float detectionRange = 10.0f; // 플레이어 감지 범위
    private Transform monster; // 플레이어의 Transform

    private void Start()
    {
        SavePosition();
        isConstruction = PlayerPrefs.GetInt("IsConstruction", 0) == 1;
        isConstructioning = PlayerPrefs.GetInt("IsConstructioning", 0) == 1;
        animator = GetComponent<Animator>();
        // 건설 진행 상태를 나타낼 UI 또는 게임 오브젝트를 가져옵니다.
        // 예시: ConstructionBar는 건설 진행 상태를 표시하는 게임 오브젝트
        if (!isConstructioning)
        {
            Slider.SetActive(false); // 초기에는 숨겨둡니다.
        }
        else
        {
            StartConstruction();
            float savedConstructionProgress = PlayerPrefs.GetFloat("ConstructionProgress", 0.0f); // 0.0f는 기본값
            constructionProgress = savedConstructionProgress;

        }
        realcunstructionTime = constructionTime * constructionTimePercent;



    }

    private void Update()
    {
        if (isUnderConstruction)
        {
            // 건설 중인 경우 타이머를 업데이트합니다.
            constructionProgress += Time.deltaTime;

            // 건설 진행 상태를 업데이트 (UI 또는 게임 오브젝트 표시)
            UpdateConstructionUI();

            // 건설이 완료되면 건물을 활성화하고 건설 상태 종료
            if (constructionProgress >= realcunstructionTime)
            {
                CompleteConstruction();
            }
        }

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
    }

    public void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            Vector3 direction = monster.position - transform.position;
            float distance = direction.magnitude;

            // 포물선 운동 계산

            if (distance < 10 && distance >= 9.4)
            {

                animator.SetTrigger("Attack");
                float arrowSpeed = 0.5f;
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
                nextAttackTime = Time.time + currentAttackCooldown;
            }


            else if (distance < 9.4 && distance >= 8.9)
            {

                animator.SetTrigger("Attack");
                float arrowSpeed = 0.51f;
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
                nextAttackTime = Time.time + currentAttackCooldown;
            }

            else if (distance < 8.9 && distance >= 8.3)
            {

                animator.SetTrigger("Attack");
                float arrowSpeed = 0.525f;
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
                nextAttackTime = Time.time + currentAttackCooldown;
            }

            else if (distance < 8.3 && distance >= 7.7)
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
                nextAttackTime = Time.time + currentAttackCooldown;
            }

            else if (distance < 7.7 && distance >= 7.1)
            {

                animator.SetTrigger("Attack");
                float arrowSpeed = 0.555f;
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
                nextAttackTime = Time.time + currentAttackCooldown;
            }

            else if (distance < 7.1 && distance >= 6.5)
            {

                animator.SetTrigger("Attack");
                float arrowSpeed = 0.575f;
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
                nextAttackTime = Time.time + currentAttackCooldown;
            }

            else if (distance < 6.5 && distance >= 5.9)
            {

                animator.SetTrigger("Attack");
                float arrowSpeed = 0.595f;
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
                nextAttackTime = Time.time + currentAttackCooldown;
            }

            else if (distance < 5.9 && distance >= 5.5)
            {

                animator.SetTrigger("Attack");
                float arrowSpeed = 0.61f;
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
                nextAttackTime = Time.time + currentAttackCooldown;
            }

            else if (distance < 5.5 && distance >= 5)
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
                nextAttackTime = Time.time + currentAttackCooldown;
            }
            
        }
    }

    // 플레이어가 건물 트리거 영역에 들어갈 때 건설 시작


    // 건설 시작
    public void StartConstruction()
    {

        isUnderConstruction = true;
        Slider.SetActive(true);
        // 다른 초기화 작업 수행
    }

    // 건설 완료
    private void CompleteConstruction()
    {
        UpGradeDelete();
        PlayerPrefs.SetInt("IsConstruction", 0);
        PlayerPrefs.Save();
        isUnderConstruction = false;
        Slider.SetActive(false);
        // 건물을 활성화하거나 다른 건설 완료 작업 수행
        gameObject.SetActive(true); // 예시: 건물을 활성화
        Vector3 spawnPosition3D = new Vector3(spawnPosition.x, spawnPosition.y, 0f);
        Justinmove justinmove = FindObjectOfType<Justinmove>();

        if (justinmove != null)
        {
            // SetTargetPosition 메서드를 호출하여 좌표를 전달
            justinmove.SetHomePosition(spawnPosition);

        }

        PlayerPrefs.SetInt("IsConstructioning", 0);
        PlayerPrefs.Save();
        PlayerPrefs.DeleteKey("ConstructionProgress"); // "ConstructionProgress" 키를 삭제
        PlayerPrefs.Save(); // 변경 사항을 저장

    }

    private void UpdateConstructionUI()
    {
        // UI 업데이트 또는 게임 오브젝트 조작을 수행하여 건설 진행 상태를 표시
        // progress 값은 0.0 (시작)에서 1.0 (완료) 사이의 값입니다.

        // "constructionBar" 게임 오브젝트의 스케일을 조절하여 체우기 효과 생성
        constructionBar.value = (float)constructionProgress / realcunstructionTime;
        PlayerPrefs.SetFloat("ConstructionProgress", constructionProgress);
        PlayerPrefs.Save();
    }

    private void SavePosition()
    {
        Vector3 position = transform.position;
        string key = "";

        if (position.x >= 2f && position.x <= 3f)
        {
            key = "2.5";
        }

        else if (position.x >= 6f && position.x <= 7f)
        {
            key = "6.5";
        }

        else if (position.x >= 10f && position.x <= 11f)
        {
            key = "10.5";
        }

        else if (position.x >= 14f && position.x <= 15f)
        {
            key = "14.5";
        }

        else if (position.x >= 18f && position.x <= 19f)
        {
            key = "18.5";
        }

        else if (position.x >= 22f && position.x <= 23f)
        {
            key = "22.5";
        }

        else if (position.x >= 26f && position.x <= 27f)
        {
            key = "26.5";
        }

        else if (position.x >= 30f && position.x <= 31f)
        {
            key = "30.5";
        }

        else if (position.x >= 34f && position.x <= 35f)
        {
            key = "34.5";
        }

        else if (position.x >= 38f && position.x <= 39f)
        {
            key = "38.5";
        }

        else if (position.x >= 42f && position.x <= 43f)
        {
            key = "42.5";
        }

        else if (position.x >= 46f && position.x <= 47f)
        {
            key = "46.5";
        }

        else if (position.x >= 50f && position.x <= 51f)
        {
            key = "50.5";
        }

        else if (position.x >= 54f && position.x <= 55f)
        {
            key = "54.5";
        }

        else if (position.x >= 58f && position.x <= 59f)
        {
            key = "58.5";
        }

        if (key != "")
        {
            PlayerPrefs.SetFloat($"{key}XCannon1", position.x);
            PlayerPrefs.SetFloat($"{key}YCannon1", position.y);
            PlayerPrefs.SetFloat($"{key}ZCannon1", position.z);
            PlayerPrefs.Save();
        }
    }

    private void UpGradeDelete()
    {
        Vector3 position = transform.position;
        string key = "";

        if (position.x >= 2f && position.x <= 3f)
        {
            key = "2.5";
        }

        else if (position.x >= 6f && position.x <= 7f)
        {
            key = "6.5";
        }

        else if (position.x >= 10f && position.x <= 11f)
        {
            key = "10.5";
        }

        else if (position.x >= 14f && position.x <= 15f)
        {
            key = "14.5";
        }

        else if (position.x >= 18f && position.x <= 19f)
        {
            key = "18.5";
        }

        else if (position.x >= 22f && position.x <= 23f)
        {
            key = "22.5";
        }

        else if (position.x >= 26f && position.x <= 27f)
        {
            key = "26.5";
        }

        else if (position.x >= 30f && position.x <= 31f)
        {
            key = "30.5";
        }

        else if (position.x >= 34f && position.x <= 35f)
        {
            key = "34.5";
        }

        else if (position.x >= 38f && position.x <= 39f)
        {
            key = "38.5";
        }

        else if (position.x >= 42f && position.x <= 43f)
        {
            key = "42.5";
        }

        else if (position.x >= 46f && position.x <= 47f)
        {
            key = "46.5";
        }

        else if (position.x >= 50f && position.x <= 51f)
        {
            key = "50.5";
        }

        else if (position.x >= 54f && position.x <= 55f)
        {
            key = "54.5";
        }

        else if (position.x >= 58f && position.x <= 59f)
        {
            key = "58.5";
        }

        if (key != "")
        {

            PlayerPrefs.DeleteKey($"{key}XCannon1");
            PlayerPrefs.DeleteKey($"{key}YCannon1");
            PlayerPrefs.DeleteKey($"{key}ZCannon1");
            PlayerPrefs.Save();

        }
        Destroy(Cannon1Prefab);
        Instantiate(Cannon2Prefab, transform.position, Quaternion.identity);
    }

}







