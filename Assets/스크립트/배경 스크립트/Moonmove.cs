using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moonmove : MonoBehaviour
{
    public Transform center;    // 원운동 중심점
    public float radius = 2.0f; // 반지름
    public float speed = 2.0f;  // 속도
    public Transform background; // 배경 Transform
    public float scrollSpeed = 1.0f; // 스크롤 속도
    public CalendarManager calendarManager; // 캘린더 관리자 추가

    private float angle = 0;
    private Vector3 initialBackgroundPosition;
    private Vector3 initialPlayerPosition;

    private void Start()
    {
        initialBackgroundPosition = background.position;
        initialPlayerPosition = transform.position; // 플레이어의 초기 위치 저장

    }



    private void Update()
    {
        // 플레이어의 움직임 변화량 계산
        float movementDelta = transform.position.x - initialPlayerPosition.x;

        // 배경을 x 방향으로 스크롤
        Vector3 backgroundPosition = initialBackgroundPosition;
        backgroundPosition.x += movementDelta * scrollSpeed;
        background.position = backgroundPosition;

        if (calendarManager != null)
        {
            float gameTimeInSeconds = calendarManager.GetGameTimeInSeconds();
            float hours = gameTimeInSeconds / 3600f;
            float initialAngleOffset = (hours - 19) * (2 * Mathf.PI / 24); // 12시를 기준으로 각도 오프셋 계산
            angle = initialAngleOffset;
        }

        // 플레이어를 원운동
        angle += speed * Time.deltaTime;
        transform.position = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
    }
}
