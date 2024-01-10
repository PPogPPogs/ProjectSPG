using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Background : MonoBehaviour
{
	public Light2D sunLight; // 2D 조명을 사용
	public Color dayColor = Color.white;
	public Color nightColor = Color.blue;
	public Color sunsetColor = Color.black;
	public float transitionDuration = 2.0f; // 색상 전환 시간 (초)
	public float dayIntensity = 1.0f;
	public float nightIntensity = 0.3f; // 밤에 감소된 강도

	private Color targetColor;
	private float targetIntensity;
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
				// 초기 색상 및 강도 설정 (색상 보간을 적용하지 않음)
				if (currentHour >= 7 && currentHour < 17)
				{
					sunLight.color = dayColor;
					sunLight.intensity = dayIntensity;
				}
				else
				{
					sunLight.color = nightColor;
					sunLight.intensity = nightIntensity;
				}
				initialColorSet = true;
			}
			else
			{
				// 이후의 색상 및 강도 변경은 딜레이가 걸립니다.
				if (currentHour >= 7 && currentHour < 17)
				{
					targetColor = dayColor;
					targetIntensity = dayIntensity;
				}
				else
				{
					targetColor = nightColor;
					targetIntensity = nightIntensity;
				}
			}
		}

		// 초기 색상 및 강도 설정 이후에는 색상 보간 및 강도 보간을 사용하여 부드럽게 전환
		if (initialColorSet)
		{
			sunLight.color = Color.Lerp(sunLight.color, targetColor, Time.deltaTime / transitionDuration);
			sunLight.intensity = Mathf.Lerp(sunLight.intensity, targetIntensity, Time.deltaTime / transitionDuration);
		}
	}
}
