using UnityEngine;
using UnityEngine.Rendering.Universal; // Light2D를 사용하기 위해 추가

public class Background : MonoBehaviour
{
    public Light2D sunLight; // 2D 조명을 사용
    public Color dayColor = Color.white;
    public Color nightColor = Color.blue;
    public Color sunsetColor = Color.black;
    public float transitionDuration = 2.0f; // 색상 전환 시간 (초)

    private Color targetColor;

    private void Start()
    {
        targetColor = sunLight.color;
    }

    private void Update()
    {
        CalendarManager calendarManager = GetComponent<CalendarManager>();

        if (calendarManager != null)
        {
            int currentHour = calendarManager.GetHour();

            if (currentHour >= 5 && currentHour < 18)
            {
                targetColor = dayColor;
            }
            else if ((currentHour >= 19 && currentHour <= 24) || currentHour < 5)
            {
                targetColor = nightColor;
            }
            else if (currentHour >= 18 && currentHour < 19)
            {
                targetColor = sunsetColor; // "warm white"에 가까운 색상을 나타냅니다.
            }
        }

        // 색상 보간을 사용하여 부드럽게 전환
        sunLight.color = Color.Lerp(sunLight.color, targetColor, Time.deltaTime / transitionDuration);
    }
}
