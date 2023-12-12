using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // 몬스터 방향으로 발사체 이동
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        // 몬스터에게 피해를 입히는 로직을 여기에 추가하세요.
        Destroy(gameObject);
    }
}
