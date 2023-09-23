using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Background : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D sunLight; // 2D 조명을 사용
    public Color dayColor = Color.white;
    public Color nightColor = Color.blue;
    public Color sunsetColor = Color.black;
    public float transitionDuration = 2.0f; // 색상 전환 시간 (초)

    private Color targetColor;
    private CalendarManager calendarManager;
    private bool initialColorSet = false;

    private void Start()
    {
        calendarManager = GetComponent<CalendarManager>();
    }

    private void Update()
    {
        if (calendarManager != null)
        {
            int currentHour = calendarManager.GetHour();

            if (!initialColorSet)
            {
                // 초기 색상 설정 (색상 보간을 적용하지 않음)
                if (currentHour >= 7 && currentHour < 17)
                {
                    sunLight.color = dayColor;
                }
                else
                {
                    sunLight.color = nightColor;
                }
                initialColorSet = true;
            }
            else
            {
                // 이후의 색상 변경은 딜레이가 걸립니다.
                if (currentHour >= 7 && currentHour < 17)
                {
                    targetColor = dayColor;
                }
                else
                {
                    targetColor = nightColor;
                }
            }
        }

        // 초기 색상 설정 이후에는 색상 보간을 사용하여 부드럽게 전환
        if (initialColorSet)
        {
            sunLight.color = Color.Lerp(sunLight.color, targetColor, Time.deltaTime / transitionDuration);
        }
    }
}

