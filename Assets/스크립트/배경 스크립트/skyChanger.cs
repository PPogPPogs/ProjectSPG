using UnityEngine;

public class skyChanger : MonoBehaviour
{
	public SpriteRenderer spriteRenderer; // 스프라이트 렌더러 컴포넌트
	public float fadeDuration = 1.0f; // fade out/in 지속 시간 (초)
	public float fadeOutStartTime = 0.0f; // fade out 시작 시간
	public float fadeInStartTime = 12.0f; // fade in 시작 시간

	private bool fadingOut = false;
	private bool fadingIn = false;

	private void Start()
	{
		// spriteRenderer가 할당되지 않은 경우 현재 GameObject에서 찾아서 할당
		if (spriteRenderer == null)
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}
	}

	private void Update()
	{
		CalendarManager calendarManager = GetComponent<CalendarManager>();

		if (calendarManager != null)
		{
			int currentHour = calendarManager.GetHour();

			if (currentHour >= fadeOutStartTime && currentHour < fadeInStartTime && !fadingOut)
			{
				Debug.Log("아침이 되었습니다.");

				fadingIn = false;
				fadingOut = true;
				StartCoroutine(FadeOutSprite());
			}
			else if (currentHour >= fadeInStartTime && !fadingIn)
			{
				Debug.Log("밤이 되었습니다.");

				fadingOut = false;
				fadingIn = true;
				StartCoroutine(FadeInSprite());
			}
		}
	}

	private System.Collections.IEnumerator FadeOutSprite()
	{
		float elapsedTime = 0.0f;
		Color startColor = spriteRenderer.color;
		Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

		while (elapsedTime < fadeDuration)
		{
			float t = elapsedTime / fadeDuration;
			spriteRenderer.color = Color.Lerp(startColor, endColor, t);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		spriteRenderer.color = endColor; // 완전한 투명으로 설정
	}

	private System.Collections.IEnumerator FadeInSprite()
	{
		float elapsedTime = 0.0f;
		Color startColor = spriteRenderer.color;
		Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);

		while (elapsedTime < fadeDuration)
		{
			float t = elapsedTime / fadeDuration;
			spriteRenderer.color = Color.Lerp(startColor, endColor, t);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		spriteRenderer.color = endColor; // 완전한 불투명으로 설정
	}
}
