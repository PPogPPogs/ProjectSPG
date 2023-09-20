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

            if (currentHour >= 7 && currentHour < 17)
            {
                targetColor = dayColor;
            }
            else if ((currentHour >= 17 && currentHour <= 24) || currentHour < 7)
            {
                targetColor = nightColor;
            }

        }

        // 색상 보간을 사용하여 부드럽게 전환
        sunLight.color = Color.Lerp(sunLight.color, targetColor, Time.deltaTime / transitionDuration);
    }
}
