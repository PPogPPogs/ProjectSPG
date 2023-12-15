using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float detectionRange = 10f; // 몬스터 감지 범위
    public string monsterTag = "LeftMonster"; // 몬스터 태그
    public Transform defaultTarget; // 몬스터가 없을 때의 기본 타겟 위치
    public GameObject cannonBallPrefab; // 대포 발사체 프리팹
    public Transform firePoint; // 대포 발사 위치
    public float fireRate = 2f; // 발사 간격 조절을 위한 변수
    private bool isInRange = false;
    private float nextFireTime = 0f;
    private Transform target;
  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            
        }
    }

    void Start()
    {
        // 시작할 때 기본 타겟 설정
        target = defaultTarget;
    }
    void Update()
    {
        // 범위 내에 있는 몬스터 찾기
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(monsterTag);

        float closestDistance = float.MaxValue;
        GameObject closestMonster = null;

        foreach (GameObject monster in monsters)
        {
            float distanceToTarget = Vector3.Distance(transform.position, monster.transform.position);

            // 몬스터가 감지 범위 안에 있고 현재 가장 가까운 몬스터보다 더 가까우면 업데이트
            if (isInRange && distanceToTarget <= detectionRange && distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                closestMonster = monster;
            }
        }

        if (closestMonster != null)
        {
            // 스페이스바를 눌렀을 때 발사
            if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
            {
                target = closestMonster.transform;
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else
        {
            // 만약 범위 내에 몬스터가 없다면 기본 타겟 사용
            target = defaultTarget;
        }
    }


    void Shoot()
    {
        // 대포 발사체를 생성하고 목표 지점으로 발사
        GameObject cannonBallObject = Instantiate(cannonBallPrefab, firePoint.position, firePoint.rotation);
        CannonBallScript cannonBallScript = cannonBallObject.GetComponent<CannonBallScript>();

        if (cannonBallScript != null)
        {
            cannonBallScript.SetTarget(target);
        }
    }
}
