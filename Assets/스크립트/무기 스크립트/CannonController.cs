using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float detectionRange = 10f; // ���� ���� ����
    public string monsterTag = "Monster"; // ���� �±�
    public Transform defaultTarget; // ���Ͱ� ���� ���� �⺻ Ÿ�� ��ġ
    public GameObject cannonBallPrefab; // ���� �߻�ü ������
    public Transform firePoint; // ���� �߻� ��ġ
    public float fireRate = 2f; // �߻� ���� ������ ���� ����
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
        // ������ �� �⺻ Ÿ�� ����
        target = defaultTarget;
    }

    void Update()
    {
        // ���� ���� �ִ� ���� ã��
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(monsterTag);

        foreach (GameObject monster in monsters)
        {
            float distanceToTarget = Vector3.Distance(transform.position, monster.transform.position);
            if (isInRange)
            {
                // ���Ͱ� ���� ���� �ȿ� �ְ�, �����̽��ٸ� ������ �� �߻�
                if (distanceToTarget <= detectionRange && Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
                {
                    target = monster.transform;
                    Shoot();
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
        }
    }

    void Shoot()
    {
        // ���� �߻�ü�� �����ϰ� ��ǥ �������� �߻�
        GameObject cannonBallObject = Instantiate(cannonBallPrefab, firePoint.position, firePoint.rotation);
        CannonBallScript cannonBallScript = cannonBallObject.GetComponent<CannonBallScript>();

        if (cannonBallScript != null)
        {
            cannonBallScript.SetTarget(target);
        }
    }
}
