using UnityEngine;
using System.Collections;

public class Justinmove : MonoBehaviour
{
    public float speed = 5.0f; // 움직임 속도
    public float minX = -5.0f; // 최소 X 위치
    public float maxX = 5.0f; // 최대 X 위치
    public float minChangeDirectionTime = 2.0f; // 방향 변경 최소 시간(초)
    public float maxChangeDirectionTime = 5.0f; // 방향 변경 최대 시간(초)

    private int direction = 1; // 초기 움직임 방향 (1: 오른쪽, -1: 왼쪽)
    private Animator animator;
    private Vector3 originalScale;

    private void Start()
    {
        // 처음으로 방향을 변경합니다.
        StartCoroutine(ChangeDirection());

        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();

        // 캐릭터의 초기 스케일 저장
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // 현재 위치를 저장합니다.
        Vector3 currentPosition = transform.position;

        // 움직임 방향에 따라 위치를 조정합니다.
        currentPosition.x += direction * speed * Time.deltaTime;

        // 새로운 위치로 이동합니다.
        transform.position = currentPosition;

        if (currentPosition.x <= minX || currentPosition.x >= maxX)
        {
            direction *= -1; // 방향을 반전시킴

            // 캐릭터 스케일을 방향에 따라 뒤집습니다.
            if (direction < 0)
            {
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
            else
            {
                transform.localScale = originalScale;
            }
        }

        // 움직임에 따라 애니메이션을 제어합니다.
        bool isRunning = Mathf.Abs(direction) > 0;
        animator.SetBool("isRunning", isRunning);
    }

    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            // 랜덤한 시간을 기다립니다.
            float changeDirectionTime = Random.Range(minChangeDirectionTime, maxChangeDirectionTime);
            yield return new WaitForSeconds(changeDirectionTime);

            // 방향을 랜덤하게 변경합니다.
            direction *= Random.Range(0, 2) == 0 ? 1 : -1; // 0 또는 1을 랜덤하게 선택하여 방향 변경

            // 캐릭터 스케일을 방향에 따라 뒤집습니다.
            if (direction < 0)
            {
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
            else
            {
                transform.localScale = originalScale;
            }
        }
    }
}
