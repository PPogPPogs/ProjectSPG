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

        // ���� �������� �߻�ü �̵�
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
        // ���Ϳ��� ���ظ� ������ ������ ���⿡ �߰��ϼ���.
        Destroy(gameObject);
    }
}
